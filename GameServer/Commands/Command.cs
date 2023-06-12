using Common.Utils;
using System.Reflection;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands
{
    /// <summary>
    ///  What methods should be overriden based on CommandType:
    ///  <para>All, Handler should override all virtual methods in Command.</para>
    ///  <para>Console, Handler should override Run method with string[] args.</para>
    ///  <para>Player, Handler should override Run method with Player / Session args.</para>
    ///  <para>When making non console command please call the Run overload with Player from Session overload.</para>
    /// </summary>
    public abstract class Command
    {
        public static readonly Logger c = new("Command", ConsoleColor.Cyan, false);
        public string Name = string.Empty;
        public string Description = string.Empty;
        public string[] Examples = Array.Empty<string>();
        public CommandType CmdType = CommandType.Player;

        /// <summary>
        /// Call this when player is online or have active session.
        /// </summary>
        /// <param name="session">The Session instance</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void Run(Session session, string[] args)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Only call this when player have no session.
        /// </summary>
        /// <param name="player">The Player instance</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void Run(Player player, string[] args)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Please only call this on ReadLine.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void Run(string[] args)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Command types used to defines what action should be taken by command interpreter or handler.
    /// </summary>
    public enum CommandType
    {
        All,
        Console,
        Player
    };

    [AttributeUsage(AttributeTargets.Class)]
    public class CommandHandler : Attribute
    {
        public string Name { get; }
        public string Description { get; }
        public string[] Examples { get; }
        public CommandType CmdType { get; }

        public CommandHandler(string name, string description, CommandType type = CommandType.Player, params string[] examples)
        {
            Name = name;
            Description = description;
            CmdType = type;
            Examples = examples;
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
                    cmd.Description = attr.Description;
                    cmd.CmdType = attr.CmdType;
                    cmd.Examples = attr.Examples;
                    Commands.Add(cmd);

#if DEBUG
                    c.Log($"Loaded Command Handler {t.Name} for Command \"{cmd.Name}\"");
#endif
                }
            }

            c.Log("Finished Loading Commands");
        }
    }
}
