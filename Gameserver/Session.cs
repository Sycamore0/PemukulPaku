using System.Net.Sockets;
using Common.Utils;

namespace PemukulPaku.GameServer
{
    public class Session
    {
        public readonly string Id;
        public readonly TcpClient Client;
        public readonly Logger c;

        public Session(string id, TcpClient client)
        {
            Id = id;
            Client = client;
            c = new Logger(Id);
            Task.Run(() => ClientLoop(client));
        }

        private void ClientLoop(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            byte[] packetMagic = { 0x01, 0x23, 0x45, 0x67 }; // Magic start pattern
            byte[] packetEnd = { 0x89, 0xAB, 0xCD, 0xEF }; // Magic end pattern

            byte[] buffer = new byte[4096];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // Process the received bytes
                    for (int i = 0; i < bytesRead; i++)
                    {
                        if (buffer[i] == packetMagic[0])
                        {
                            bool found = true;
                            for (int j = 1; j < packetMagic.Length; j++)
                            {
                                if (buffer[i + j] != packetMagic[j])
                                {
                                    found = false;
                                    break;
                                }
                            }

                            if (found)
                            {
                                // Magic start pattern found
                                int endIndex = Array.IndexOf(buffer, packetEnd[0], i + packetMagic.Length);
                                if (endIndex != -1)
                                {
                                    // Magic end pattern found
                                    int packetLength = endIndex - i + packetEnd.Length;
                                    byte[] packet = new byte[packetLength];
                                    Array.Copy(buffer, i, packet, 0, packetLength);

                                    // Process the packet
                                    ProcessPacket(packet);

                                    // Update the buffer
                                    int remainingBytes = bytesRead - (i + packetLength);
                                    Array.Copy(buffer, i + packetLength, buffer, 0, remainingBytes);

                                    // Adjust the bytesRead value accordingly
                                    bytesRead = remainingBytes;
                                    i = -1; // Start processing from the beginning of the buffer again
                                }
                                else
                                {
                                    // End pattern not found, break the loop and wait for more data
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (Client != null)
                {
                    c.Warn($"{Id} disconnected");
                    Server.GetInstance().Sessions.Remove(Id);
                    c.Debug("TCP client disconnect reason: " + ex.Message);
                }
                else
                {
                    c.Error("TCP client error: " + ex.Message);
                }
            }
            finally { Server.GetInstance().LogClients(); };
        }

        public void ProcessPacket(byte[] packet)
        {
            _ = new Packet(packet);
        }
    }
}
