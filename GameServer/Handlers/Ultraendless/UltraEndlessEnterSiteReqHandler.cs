using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.UltraEndlessEnterSiteReq)]
    internal class UltraEndlessEnterSiteReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            UltraEndlessEnterSiteReq Data = packet.GetDecodedBody<UltraEndlessEnterSiteReq>();

            session.Send(Packet.FromProto(new UltraEndlessEnterSiteRsp() { retcode = UltraEndlessEnterSiteRsp.Retcode.Succ, SiteId = Data.SiteId }, CmdId.UltraEndlessEnterSiteRsp));
        }
    }
}
