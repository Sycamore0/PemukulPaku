using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.StageInnerDataReportReq)]
    internal class StageInnerDataReportReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new StageInnerDataReportRsp() { retcode = StageInnerDataReportRsp.Retcode.Succ }, CmdId.StageInnerDataReportRsp));
        }
    }
}
