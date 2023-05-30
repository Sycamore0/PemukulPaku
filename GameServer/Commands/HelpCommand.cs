namespace PemukulPaku.GameServer.Commands
{

    [CommandHandler("help", "Shows the help page", CommandType.All)]
    internal class HelpCommand : Command
    {
        public override void Run(Session session, string[] args)
        {
            // TODO: Implement online help
            base.Run(session, args);
        }

        public override void Run(string[] args)
        {
            foreach (Command Cmd in CommandFactory.Commands)
            {
                Console.ForegroundColor= ConsoleColor.White;
                Console.WriteLine("      " + Cmd.Name);
                Console.ResetColor();
                c.Trail(Cmd.Description);
            }
        }
    }
}
