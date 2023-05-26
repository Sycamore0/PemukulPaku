using Common.Resources.Proto;
using Common;
using System.Net.NetworkInformation;
using PemukulPaku.GameServer;

namespace PemukulPaku
{
    class Program
    {
        public static void Main()
        {
            Global.c.Log("Starting...");

            Global.config.Gameserver.Host = NetworkInterface.GetAllNetworkInterfaces().Where(i => i.NetworkInterfaceType != NetworkInterfaceType.Loopback && i.OperationalStatus == OperationalStatus.Up).First().GetIPProperties().UnicastAddresses.Where(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First().Address.ToString();

            GetPlayerTokenRsp getPlayerTokenRsp = new()
            {
                Msg = "Hello!"
            };

            new Thread(HttpServer.Program.Main).Start();
            PacketFactory.LoadPacketHandlers();
            _ = Server.GetInstance();

            Console.Read();
        }
    }
}