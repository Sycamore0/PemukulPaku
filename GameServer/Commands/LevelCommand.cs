using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Commands
{
    [CommandHandler("level", "<1-88>", CommandType.Player)]
    internal class LevelCommand : Command
    {
        public override void Run(Session session, string[] args)
        {
            Run(session.Player, args);

            GetMainDataRsp Rsp = new()
            {
                retcode = GetMainDataRsp.Retcode.Succ,
                Level = (uint)PlayerLevelData.GetInstance().CalculateLevel(session.Player.User.Exp).Level,
                Exp = (uint)PlayerLevelData.GetInstance().CalculateLevel(session.Player.User.Exp).Exp,
                TypeLists = new uint[] { 3, 4 }
            };
            session.Send(Packet.FromProto(Rsp, CmdId.GetMainDataRsp), Packet.FromProto(new PlayerLevelUpNotify() { NewLevel = Rsp.Level }, CmdId.PlayerLevelUpNotify));
        }

        public override void Run(Player player, string[] args)
        {
            int level = int.Parse(args[0]);
            player.User.Exp = PlayerLevelData.GetInstance().CalculateExpForLevel(level).Exp;
            player.User.Save();
        }
    }
}
