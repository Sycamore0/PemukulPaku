using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.UltraEndlessGetMainDataReq)]
    internal class UltraEndlessGetMainDataReqHandler : IPacketHandler
    {
        
        public void Handle(Session session, Packet packet)
        {
            uint bracket = session.Player.User.AbyssGroupLevel;
            uint temp = session.Player.User.AbyssDynamicHard;
            UltraEndlessGetMainDataRsp Rsp = new()
            {
                retcode = UltraEndlessGetMainDataRsp.Retcode.Succ,
                ScheduleId = 1028,
                GroupLevel = bracket,
                TopGroupLevel = 9,
                CupNum = 2000,
                MainData = new()
                {
                    ScheduleId = 1028,
                    EffectTime = 0,
                    BeginTime = 0,
                    EndTime = (uint)Global.GetUnixInSeconds() + 3600 * 24 * 7,
                    CloseTime = (uint)Global.GetUnixInSeconds() + 3600 * 24 * 7 + 1200,
                    CurSeasonId = 1
                },
                DynamicHardLevel = temp,
                LastSettleInfo = new() { }
            };
            Rsp.MainData.SiteLists.AddRange(Common.Utils.ExcelReader.UltraEndlessSite.GetInstance().All.Where(x => x.SiteId > 1019).Select(siteData =>
            {
                List<UltraEndlessFloorExcel> floorDatas = Common.Utils.ExcelReader.UltraEndlessFloor.GetInstance().GetFloorDatasFromStageId(siteData.StageId);
                var siteInfo = new Common.Resources.Proto.UltraEndlessSite() { SiteId = (uint)siteData.SiteId };
                siteInfo.FloorLists.AddRange(floorDatas.Select(x => new Common.Resources.Proto.UltraEndlessFloor() { Floor = (uint)x.FloorId, MaxScore = 0 }));

                return siteInfo; 
            }));

            session.Send(Packet.FromProto(Rsp, CmdId.UltraEndlessGetMainDataRsp));
        }
    }
}
