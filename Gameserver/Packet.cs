using System.Buffers.Binary;
using Common.Resources.Proto;
using Newtonsoft.Json;
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
                c.Error($"CMD ID {CmdId} NOT RECOGNIZED!");
            }

            HeadMagic = buf.Take(4).ToArray();
            UserId = BinaryPrimitives.ReadUInt32BigEndian(buf.AsSpan(12));
            ushort headerLen = BinaryPrimitives.ReadUInt16BigEndian(buf.AsSpan(28));
            BodyLen = BinaryPrimitives.ReadUInt32BigEndian(buf.AsSpan(30));
            Body = buf.Skip(34 + headerLen).Take((int)BodyLen).ToArray();
            TailMagic = buf.Skip(buf.Length - 4).ToArray();

            try
            {
                MemoryStream ms = new(Body);
                object SerializedBody = Serializer.NonGeneric.Deserialize(typeof(Common.Global).Assembly.GetType($"Common.Resources.Proto.{PacketName}"), ms)!;
                c.Debug(JsonConvert.SerializeObject(SerializedBody));
            }
            catch
            {
                c.Error($"Failed to deserialized packet with Common.Resources.Proto.{PacketName}");
            }
        }
    }

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

                c.Log($"Loaded PacketHandler {t.Name} for Packet Type {attr.Id}");
            }

            c.Log("Finished Loading Packet Handlers");
        }
    }

    [PacketCmdId(CmdId.PlayerLoginReq)]
    public class PlayerLoginReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            throw new NotImplementedException();
        }
    }
}
