using Common;

namespace PemukulPaku.GameServer.Commands
{
    [CommandHandler("test")]
    public class TestCommand : Command
    {
        public override void Run(Session? session, string[] args)
        {
            if (args.Length < 3)
            {
                c.Error("Not enough arguments");
                return;
            }

            c.Log($"Testing {args[0]} {args[1]} {args[2]}");
        }
    }
}
