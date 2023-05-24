using Common.Resources.Proto;
using Common;

namespace PemukulPaku
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello!");
            GetPlayerTokenRsp getPlayerTokenRsp = new()
            {
                Msg = "Hello!"
            };
            Console.WriteLine(Global.config.Gameserver.Host);
            new Thread(HttpServer.Program.Main).Start();
            Console.ReadKey(true);
        }
    }
}