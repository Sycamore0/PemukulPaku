using System.Data.SqlTypes;
using System;
using System.IO;
using System.Net.Sockets;
using Common;
using Common.Resources.Proto;
using Common.Utils;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer
{
    public class Session
    {
        public readonly string Id;
        public readonly TcpClient Client;
        public readonly Logger c;
        public Player Player = default!;

        public Session(string id, TcpClient client)
        {
            Id = id;
            Client = client;
            c = new Logger(Id);
            Task.Run(ClientLoop);
        }

        private void ClientLoop()
        {
            NetworkStream stream = Client.GetStream();

            byte[] msg = new byte[1 << 16];

            while (Client.Connected)
            {
                Array.Clear(msg, 0, msg.Length);
                int len = stream.Read(msg, 0, msg.Length);

                if (len > 0)
                {
                    List<byte[]> packets = new ();

                    ReadOnlySpan<byte> prefix = new byte[] { 0x01, 0x23, 0x45, 0x67 };
                    ReadOnlySpan<byte> suffix = new byte[] { 0x89, 0xAB, 0xCD, 0xEF };

                    Span<byte> message = msg.AsSpan();

                    for (int offset = 0; offset < message.Length;)
                    {
                        var segment = message[offset..];
                        int start = segment.IndexOf(prefix);

                        if (start == -1)
                            break;

                        int end = segment.IndexOf(suffix);

                        if (end == -1)
                            break;

                        end += suffix.Length;

                        packets.Add(segment[start..end].ToArray());
                        offset += end;
                    }

                    foreach (byte[] packet in packets)
                    {
                        if (Packet.IsValid(packet))
                        {
                            ProcessPacket(packet);
                        }
                        else
                        {
                            c.Error("Invalid packet received:", BitConverter.ToString(packet).Replace("-", ""));
                        }
                    }
                }
            }

            c.Debug("ClientLoop ends");
            Server.GetInstance().LogClients();

            c.Warn($"{Id} disconnected");
            Server.GetInstance().Sessions.Remove(Id);
        }

        public void ProcessPacket(byte[] packet)
        {
            Packet _packet = new(packet);
            string PacketName = Enum.GetName(typeof(CmdId), _packet.CmdId)!;
            try
            {
                CmdId cmdId = (CmdId)Enum.ToObject(typeof(CmdId), _packet.CmdId);
                IPacketHandler? handler = PacketFactory.GetPacketHandler(cmdId);

                if (handler == null)
                {
                    c.Warn($"{PacketName} not handled!");
                    return;
                }

                c.Log(PacketName);

                handler.Handle(this, _packet);
            }
            catch(Exception ex) 
            { 
                if ((int)Global.config.VerboseLevel > 0)
                {
                    c.Error(ex.Message); 
                }
            }
        }

        public void Send(params Packet[] packets)
        {
            foreach (Packet packet in packets)
            {
                string PacketName = Enum.GetName(typeof(CmdId), packet.CmdId)!;

                try
                {
                    Client.GetStream().Write(packet.Raw, 0, packet.Raw.Length);
                    c.Log(PacketName);
                }
                catch (Exception ex)
                {
                    c.Error($"Failed to send {PacketName}:" + ex.Message);
                }
            }
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
