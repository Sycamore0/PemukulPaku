using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetExBossRankReq)]
    internal class GetExBossRankReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetExBossRankReq Data = packet.GetDecodedBody<GetExBossRankReq>();
            GetExBossRankRsp Rsp = new()
            {
                retcode = GetExBossRankRsp.Retcode.Succ,
                BossId = Data.BossId,
                RankId = Data.RankId,
                RankData = new()
                {
                    MyScore = 0,
                    MyRank = 1,
                    MyRankType = 1
                }
            };

            session.Send(Packet.FromProto(Rsp, CmdId.GetExBossRankRsp));
        }
    }
}
