using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands
{
    [CommandHandler("abyss", "<sel> [#]", CommandType.Player,"temp 400", "bracket [1-9]")]
    internal class AbyssCommand : Command
    {
        public override void Run(Session session, string[] args)
        {
            Run(session.Player, args);

            //session.ProcessPacket(Packet.FromProto(new UltraEndlessGetMainDataReq() { }, CmdId.UltraEndlessGetMainDataReq));
        }
        public override void Run(Player player, string[] args)
        {
            string action = args[0];
            uint value = args[1] is not null ? uint.Parse(args[1]) : 0;

            switch (action)
            {
                case "disturbance":
                case "d":
                case "temp":
                    player.User.AbyssDynamicHard = value;
                    break;
                case "bracket": 
                case "group": 
                    player.User.AbyssGroupLevel = value > 0 && value < 10 ? value : 9;
                    break;
                default: throw new ArgumentException("Unrecognized action");
            }

            player.User.Save();
        }
    }
}
