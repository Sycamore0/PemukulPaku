using System.Buffers.Binary;
using Common.Resources.Proto;
using Common.Utils;
using ProtoBuf;
using System.Reflection;

namespace PemukulPaku.GameServer
{
    public class Packet
    {
        public readonly byte[] Raw;
        public readonly byte[] HeadMagic;
        public readonly uint UserId;
        public readonly uint CmdId;
        public readonly uint BodyLen;
        public readonly byte[] Body;
        public readonly byte[] TailMagic;
        public static readonly Logger c = new("Packet", ConsoleColor.Magenta);

        public Packet(byte[] buf)
        {
            Raw = buf;
            CmdId = BinaryPrimitives.ReadUInt32BigEndian(buf.AsSpan(24));
            string? PacketName = Enum.GetName(typeof(CmdId), CmdId);

            if (PacketName == null)
            {
                c.Warn($"CmdId {CmdId} not recognized!");
            }

            HeadMagic = buf.Take(4).ToArray();
            UserId = BinaryPrimitives.ReadUInt32BigEndian(buf.AsSpan(12));
            ushort headerLen = BinaryPrimitives.ReadUInt16BigEndian(buf.AsSpan(28));
            BodyLen = BinaryPrimitives.ReadUInt32BigEndian(buf.AsSpan(30));
            Body = buf.Skip(34 + headerLen).Take((int)BodyLen).ToArray();
            TailMagic = buf.Skip(buf.Length - 4).ToArray();
        }

        public T GetDecodedBody<T>()
        {
            T SerializedBody = default!;

            try
            {
                MemoryStream ms = new(Body);
                SerializedBody = Serializer.Deserialize<T>(ms)!;
            }
            catch
            {
                string? PacketName = Enum.GetName(typeof(CmdId), CmdId);
                c.Error($"Failed to deserialize {PacketName ?? CmdId.ToString()}!");
            }
            return SerializedBody;
        }

        public static bool IsValid(byte[] data)
        {
            string hexString = BitConverter.ToString(data).Replace("-", "");
            return hexString.StartsWith("01234567", StringComparison.OrdinalIgnoreCase) &&
                   hexString.EndsWith("89ABCDEF", StringComparison.OrdinalIgnoreCase);
        }

        public static Packet FromProto<T>(T proto, CmdId cmdId)
        {
            MemoryStream stream = new ();
            Serializer.Serialize(stream, proto);
            byte[] data = stream.ToArray();

            byte[] buf = new byte[38 + data.Length];
            Array.Fill(buf, (byte)0);

            BinaryPrimitives.WriteUInt32BigEndian(buf, 0x1234567);
            BinaryPrimitives.WriteUInt16BigEndian(buf.AsSpan(4), 1);
            BinaryPrimitives.WriteUInt16BigEndian(buf.AsSpan(6), 0);
            BinaryPrimitives.WriteUInt32BigEndian(buf.AsSpan(8), 0);
            BinaryPrimitives.WriteUInt32BigEndian(buf.AsSpan(12), 0);
            BinaryPrimitives.WriteUInt32BigEndian(buf.AsSpan(16), 0);
            BinaryPrimitives.WriteUInt32BigEndian(buf.AsSpan(20), 0);
            BinaryPrimitives.WriteUInt32BigEndian(buf.AsSpan(24), (uint)cmdId);
            BinaryPrimitives.WriteUInt16BigEndian(buf.AsSpan(28), 0);
            BinaryPrimitives.WriteUInt32BigEndian(buf.AsSpan(30), (uint)data.Length);
            data.CopyTo(buf.AsSpan(34));
            BinaryPrimitives.WriteUInt32BigEndian(buf.AsSpan(34 + data.Length), 0x89abcdef);

            return new Packet(buf);
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PacketCmdId : Attribute
    {
        public CmdId Id { get; }

        public PacketCmdId(CmdId id)
        {
            Id = id;
        }
    }

    public interface IPacketHandler
    {
        public void Handle(Session session, Packet packet);
    }

    public static class PacketFactory
    {
        public static readonly Dictionary<CmdId, IPacketHandler> Handlers = new();
        static readonly Logger c = new("Factory", ConsoleColor.Yellow);

        public static void LoadPacketHandlers()
        {
            c.Log("Loading Packet Handlers...");

            IEnumerable<Type> classes = from t in Assembly.GetExecutingAssembly().GetTypes()
                                        select t;

            foreach ((Type t, PacketCmdId attr) in from Type? t in classes.ToList()
                                                   let attrs = (Attribute[])t.GetCustomAttributes(typeof(PacketCmdId), false)
                                                   where attrs.Length > 0
                                                   let attr = (PacketCmdId)attrs[0]
                                                   where !Handlers.ContainsKey(attr.Id)
                                                   select (t, attr))
            {
                Handlers.Add(attr.Id, (IPacketHandler)Activator.CreateInstance(t)!);
#if DEBUG
                c.Log($"Loaded PacketHandler {t.Name} for Packet Type {attr.Id}");
#endif
            }

            c.Log("Finished Loading Packet Handlers");
        }

        public static IPacketHandler? GetPacketHandler(CmdId cmdId)
        {
            Handlers.TryGetValue(cmdId, out IPacketHandler? handler);
            return handler;
        }
    }
}
