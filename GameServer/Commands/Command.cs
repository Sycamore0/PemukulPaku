using Common.Utils;
using System.Reflection;

namespace PemukulPaku.GameServer.Commands
{
    public abstract class Command
    {
        public static readonly Logger c = new("Command", ConsoleColor.Cyan);
        public string Name = string.Empty;

        public virtual void Run(Session? session, string[] args)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class CommandHandler : Attribute
    {
        public string Name { get; }

        public CommandHandler(string name)
        {
            Name = name;
        }
    }

    public static class CommandFactory
    {
        public static readonly List<Command> Commands = new();
        static readonly Logger c = new("Factory", ConsoleColor.Yellow);

        public static void LoadCommandHandlers()
        {
            c.Log("Loading Command Handlers...");

            IEnumerable<Type> classes = from t in Assembly.GetExecutingAssembly().GetTypes()
                                        select t;

            foreach ((Type t, CommandHandler attr) in from Type? t in classes.ToList()
                                                      let attrs = (Attribute[])t.GetCustomAttributes(typeof(CommandHandler), false)
                                                      where attrs.Length > 0
                                                      let attr = (CommandHandler)attrs[0]
                                                      select (t, attr))
            {
                Command? cmd = (Command)Activator.CreateInstance(t)!;

                if (cmd is not null)
                {
                    cmd.Name = attr.Name;
                    Commands.Add(cmd);

#if DEBUG
                    c.Log($"Loaded Command Handler {t.Name} for Command \"{cmd.Name}\"");
#endif
                }
            }

            c.Log("Finished loading Commands");
        }
    }
}
