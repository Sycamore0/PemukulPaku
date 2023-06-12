using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Two
{
    [PacketCmdId(CmdId.MpGetMatchInfoReq)]
    internal class MpGetMatchInfoReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new MpGetMatchInfoRsp() { retcode = MpGetMatchInfoRsp.Retcode.Succ, LobbyIdx = 1 }, CmdId.MpGetMatchInfoRsp));
        }
    }
}
