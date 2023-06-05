using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.ThemeWantedRefreshTicketReq)]
    internal class ThemeWantedRefreshTicketReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new ThemeWantedRefreshTicketRsp() { retcode = ThemeWantedRefreshTicketRsp.Retcode.Succ }, CmdId.ThemeWantedRefreshTicketRsp));
        }
    }
}
