namespace PemukulPaku.GameServer.Commands
{
    [CommandHandler("target", "[id], displays and selects User sessions for commands", CommandType.Console)]
    internal class TargetCommand : Command
    {
        public override void Run(string[] args)
        {
            if(args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Selected Session: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(ReadLine.GetInstance().session?.Id ?? "None");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Active Sessions:");
                Console.ResetColor();
                foreach (KeyValuePair<string, Session> session in Server.GetInstance().Sessions)
                {
                    c.Trail(session.Key);
                }
            } else
            {
                if(Server.GetInstance().Sessions.TryGetValue(args[0], out Session? session))
                {
                    ReadLine.GetInstance().session = session;
                    c.Log("Session set to " + session.Id);
                }
                else
                {
                    c.Error("Session not found with key " + args[0]);
                }
            }
        }
    }
}
