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

            byte[] packetMagic = { 0x01, 0x23, 0x45, 0x67 }; // Magic start pattern
            byte[] packetEnd = { 0x89, 0xAB, 0xCD, 0xEF }; // Magic end pattern

            byte[] msg = new byte[1 << 16];

            while (Client.Connected)
            {
                Array.Clear(msg, 0, msg.Length);
                int len = stream.Read(msg, 0, msg.Length);

                if(len > 0)
                {
                    List<byte[]> packets = new ();

                    string CursedMsg = BitConverter.ToString(msg).Replace("-", "");
                    string CursedMagic = BitConverter.ToString(packetMagic).Replace("-", "");
                    string CursedEnd = BitConverter.ToString(packetEnd).Replace("-", "");

                    int MagicIndex = 0;
                    int EndIndex = 0;

                    while ((MagicIndex = CursedMsg.IndexOf(CursedMagic, MagicIndex)) != -1 && (EndIndex = CursedMsg.IndexOf(CursedEnd, EndIndex)) != -1)
                    {
                        EndIndex += 8;
                        byte[] packet = new byte[EndIndex / 2 - MagicIndex / 2];
                        Array.Copy(msg, MagicIndex / 2, packet, 0, EndIndex / 2 - MagicIndex / 2);
                        packets.Add(packet);
                        MagicIndex += MagicIndex;
                        EndIndex += EndIndex;
                    }

                    c.Debug($"Found {packets.Count} packet");

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
