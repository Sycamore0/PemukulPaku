using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.UltraEndlessReportSiteFloorReq)]
    internal class UltraEndlessReportSiteFloorReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            UltraEndlessReportSiteFloorReq Data = packet.GetDecodedBody<UltraEndlessReportSiteFloorReq>();
            // Common.Utils.ExcelReader.UltraEndlessSiteExcel? siteData = Common.Utils.ExcelReader.UltraEndlessSite.GetInstance().FromId((int)Data.SiteId);

            session.Send(Packet.FromProto(new UltraEndlessReportSiteFloorRsp()
            {
                retcode = UltraEndlessReportSiteFloorRsp.Retcode.Succ,
                Floor = Data.Floor,
                SiteId = Data.SiteId,
                IsUpFloor = false
            }, CmdId.UltraEndlessReportSiteFloorRsp));
        }
    }
}
