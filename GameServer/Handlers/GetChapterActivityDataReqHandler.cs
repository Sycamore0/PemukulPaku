using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetChapterActivityDataReq)]
    internal class GetChapterActivityDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new GetChapterActivityDataRsp() { retcode = GetChapterActivityDataRsp.Retcode.Succ }, CmdId.GetChapterActivityDataRsp));
        }
    }
}
