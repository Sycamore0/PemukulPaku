using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Two
{
    [PacketCmdId(CmdId.GetTrialAvatarReq)]
    internal class GetTrialAvatarReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new GetTrialAvatarRsp() { retcode = GetTrialAvatarRsp.Retcode.Succ, IsAllUpdate = true }, CmdId.GetTrialAvatarRsp));
        }
    }
}
