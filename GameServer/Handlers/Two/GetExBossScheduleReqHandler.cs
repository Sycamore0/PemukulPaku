using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetExBossScheduleReq)]
    internal class GetExBossScheduleReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetExBossScheduleRsp Rsp = new()
            {
                retcode = GetExBossScheduleRsp.Retcode.Succ,
                BeginTime = 0,
                EndTime = (uint)Global.GetUnixInSeconds() + 604800,
                MinLevel = 0,
                ScheduleId = 1
            };
            Rsp.RankId = PlayerLevelData.GetInstance().ExBossRankFromExp(session.Player.User.Exp);

            session.Send(Packet.FromProto(Rsp, CmdId.GetExBossScheduleRsp));
        }
    }
}
