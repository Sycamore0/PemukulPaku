using System.Net.Sockets;
using Common;
using Common.Resources.Proto;
using Common.Utils;
using PemukulPaku.GameServer.Commands;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer
{
    public class Session
    {
        public readonly string Id;
        public readonly TcpClient Client;
        public readonly Logger c;
        public long LastClientKeepAlive = Global.GetUnixInSeconds();
        public long LastServerKeepAlive = Global.GetUnixInSeconds();
        public Player Player = default!;

        public Session(string id, TcpClient client)
        {
            Id = id;
            Client = client;
            c = new Logger(Id);
            Task.Run(ClientLoop);
            new Thread(KeepAliveLoop).Start();
        }

        private void ClientLoop()
        {
            NetworkStream stream = Client.GetStream();

            byte[] msg = new byte[1 << 16];

            while (Client.Connected)
            {
                try
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
                                ProcessPacket(new Packet(packet), true);
                            }
                            else
                            {
                                c.Error("Invalid packet received:", BitConverter.ToString(packet).Replace("-", ""));
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }

            DisconnectProtocol();
        }

        public void KeepAliveLoop()
        {
            while(Client.Connected)
            {
                try
                {
                    Send(Packet.FromProto(new KeepAliveNotify() { }, CmdId.KeepAliveNotify));
                    LastServerKeepAlive = Global.GetUnixInSeconds();
                }
                catch
                {
                    break;
                }
                Thread.Sleep(3000);
            }

            DisconnectProtocol();
        }

        public void DisconnectProtocol()
        {
            if (Server.GetInstance().Sessions.GetValueOrDefault(Id) is null)
                return;

            Player.SaveAll();
            c.Debug("Player data saved to database");
            c.Warn($"{Id} disconnected");

            if (ReadLine.GetInstance().session == this) { ReadLine.GetInstance().session = null; }
            Server.GetInstance().Sessions.Remove(Id);
            Server.GetInstance().LogClients();
        }

        public void ProcessPacket(Packet _packet, bool log = false)
        {
            string PacketName = Enum.GetName(typeof(CmdId), _packet.CmdId)!;
            if(PacketName == "KeepAliveNotify") { LastClientKeepAlive = Global.GetUnixInSeconds(); c.Log(PacketName); return; }
            try
            {
                CmdId cmdId = (CmdId)Enum.ToObject(typeof(CmdId), _packet.CmdId);
                IPacketHandler? handler = PacketFactory.GetPacketHandler(cmdId);

                if (handler == null)
                {
                    c.Warn($"{PacketName} not handled!");
                    return;
                }

                if(log)
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
                catch (ObjectDisposedException)
                {
                    DisconnectProtocol();
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

        public static Session? FromUid(uint uid)
        {
            return Server.GetInstance().Sessions.FirstOrDefault(s => s.Value.Player.User.Uid == uid).Value;
        }
    }
}
