using System.Net.Sockets;
using System.Net;
using Common;
using Common.Utils;

namespace PemukulPaku.Gameserver
{
    public class Server
    {
        public static readonly Logger c = new("TCP", ConsoleColor.Blue);

        public static void Start()
        {
            TcpListener Listener = new(IPAddress.Parse("0.0.0.0"), (int)Global.config.Gameserver.Port);

            try
            {
                Listener.Start();
                c.Log($"TCP server started on port {Global.config.Gameserver.Port}");

                while (true)
                {
                    TcpClient Client = Listener.AcceptTcpClient();
                    c.Warn($"{Client.Client.RemoteEndPoint} connected!");
                    NetworkStream stream = Client.GetStream();
                }
            }
            catch (Exception ex)
            {
                c.Error("TCP server error: " + ex.Message);
            }
        }
    }
}
