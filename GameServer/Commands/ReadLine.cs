namespace PemukulPaku.GameServer.Commands
{
    public class ReadLine
    {
        public static ReadLine? Instance { get; private set; }
        public Session? session = null;

        public static ReadLine GetInstance()
        {
            return Instance ??= new();
        }

        public void Start()
        {
            while (true)
            {
                string? line = Console.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    List<string> args = line.Split(' ').ToList();
                    Command? Cmd = CommandFactory.Commands.Find(cmd => args[0] == cmd.Name.ToLower());

                    if(Cmd != null)
                    {
                        args.RemoveAt(0);

                        if (Cmd.CmdType == CommandType.All)
                        {
                            Cmd.Run(args.ToArray());
                        }
                        else if(session != null)
                        {

                            Command.c.Log("Command executed");
                        }
                        else
                        {
                            Command.c.Error("Invalid usage, try selecting session first with target");
                            continue;
                        }

                        continue;
                    }
                    else
                    {
                        Command.c.Error("Command not found, try using help");
                    }
                }
            }
        }
    }
}
