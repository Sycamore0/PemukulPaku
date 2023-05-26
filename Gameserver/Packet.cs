using System.Buffers.Binary;
using Common.Resources.Proto;
using Newtonsoft.Json;
using Common.Utils;
using ProtoBuf;

namespace PemukulPaku.Gameserver
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
}
