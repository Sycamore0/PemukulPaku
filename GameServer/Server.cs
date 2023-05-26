using System.Net.Sockets;
using System.Net;
using Common;
using Common.Utils;

namespace PemukulPaku.GameServer
{
    public class Server
    {
        public static readonly Logger c = new("TCP", ConsoleColor.Blue);
        public readonly Dictionary<string, Session> Sessions = new();
        private static Server? Instance;

        public static Server GetInstance()
        {
            return Instance ??= new Server();
        }

        public Server()
        {
            Task.Run(Start);
        }

        public void Start()
        {
            TcpListener Listener = new(IPAddress.Parse("0.0.0.0"), (int)Global.config.Gameserver.Port);

            while (true)
            {
                try
                {
                    Listener.Start();
                    c.Log($"TCP server started on port {Global.config.Gameserver.Port}");

                    while (true)
                    {
                        TcpClient Client = Listener.AcceptTcpClient();
                        string Id = Client.Client.RemoteEndPoint!.ToString()!;

                        c.Warn($"{Id} connected");
                        Sessions.Add(Id, new Session(Id, Client));
                        LogClients();
                    }
                }
                catch (Exception ex)
                {
                    c.Error("TCP server error: " + ex.Message);
                    Thread.Sleep(3000);
                }
            }
        }

        public void LogClients()
        {
            c.Log($"Connected clients: {Sessions.Count}");
        }
    }
}
