using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands
{
    [CommandHandler("abyss", "", CommandType.Player)]
    internal class AbyssCommand : Command
    {
        public override void Run(Session session, string[] args)
        {
            Run(session.Player, args);

            // session.ProcessPacket(Packet.FromProto(new UltraEndlessGetMainDataReq() { }, CmdId.UltraEndlessGetMainDataReq));
        }
        public override void Run(Player player, string[] args)
        {
            string action = args[0];
            uint value = args[1] is not null ? uint.Parse(args[1]) : 0;

            switch (action.ToLower())
            {
                case "temp":
                    player.User.AbyssDynamicHard = value;
                    break;
                default:
                    throw new ArgumentException("Unrecognized action");
            }

            player.User.Save();
        }
    }
}