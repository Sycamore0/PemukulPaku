using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.UltraEndlessGetTopRankReq)]
    internal class UltraEndlessGetTopRankReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            UltraEndlessGetTopRankReq Data = packet.GetDecodedBody<UltraEndlessGetTopRankReq>();
            UltraEndlessGetTopRankRsp Rsp = new()
            {
                retcode = UltraEndlessGetTopRankRsp.Retcode.Succ,
                ScheduleId = Data.ScheduleId,
                RankData = new()
                {
                    IsFeatureClosed = true,
                    MyRank = 1,
                    MyRankType = 1,
                    MyScore = 0
                }
            };

            session.Send(Packet.FromProto(Rsp, CmdId.UltraEndlessGetTopRankRsp));
        }
    }
}
