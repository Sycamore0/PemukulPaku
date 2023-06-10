using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.One
{
    [PacketCmdId(CmdId.MpMemberSetClientStatusReq)]
    internal class MpMemberSetClientStatusReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new MpMemberSetClientStatusRsp() { retcode = MpMemberSetClientStatusRsp.Retcode.Succ }, CmdId.MpMemberSetClientStatusRsp));
        }
    }
}
