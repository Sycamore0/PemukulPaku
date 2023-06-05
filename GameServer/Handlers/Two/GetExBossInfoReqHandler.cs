using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetExBossInfoReq)]
    internal class GetExBossInfoReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetExBossInfoRsp Rsp = new()
            {
                retcode = GetExBossInfoRsp.Retcode.Succ,
                BossInfo = new()
                {
                    EnterTimes = 0,
                    ScheduleId = 1,
                    MaxSweepLevel = 4
                }
            };
            Rsp.BossInfo.RankId = PlayerLevelData.GetInstance().ExBossRankFromExp(session.Player.User.Exp);
            foreach (ExBossMonsterDataExcel monsterData in ExBossMonsterData.GetInstance().All.Where(monster => monster.ConfigId == Rsp.BossInfo.RankId))
            {
                Rsp.BossInfo.BossIdLists.Add(new() { BossId = (uint)monsterData.BossId });
            }

            session.Send(Packet.FromProto(Rsp, CmdId.GetExBossInfoRsp));
        }
    }
}
