using Common;
using System.Net.NetworkInformation;
using PemukulPaku.GameServer;
using Common.Database;
using PemukulPaku.GameServer.Game;
using PemukulPaku.GameServer.Commands;
using MongoDB.Bson;
using Common.Resources.Proto;

namespace PemukulPaku
{
    class Program
    {
        public static void Main()
        {
#if DEBUG
            Global.config.VerboseLevel = VerboseLevel.Debug;
#endif
            Global.c.Log("Starting...");

            if (Global.config.Gameserver.Host == "127.0.0.1")
                Global.config.Gameserver.Host = NetworkInterface.GetAllNetworkInterfaces().Where(i => i.NetworkInterfaceType != NetworkInterfaceType.Loopback && i.OperationalStatus == OperationalStatus.Up).First().GetIPProperties().UnicastAddresses.Where(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First().Address.ToString();

            CommandFactory.LoadCommandHandlers();
            PacketFactory.LoadPacketHandlers();
            new Thread(HttpServer.Program.Main).Start();
            _ = Server.GetInstance();

            Player Player = new(User.FromName("test"));

            ReadLine.GetInstance().Start();
        }
    }
}