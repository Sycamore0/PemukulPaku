using PemukulPaku.Resources.Proto;

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
        }
    }
}