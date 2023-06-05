using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetThemeWantedReq)]
    internal class GetThemeWantedReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetThemeWantedRsp Rsp = new()
            {
                retcode = GetThemeWantedRsp.Retcode.Succ,
                ThemeWantedActivity = new()
                {
                    ScheduleId = 1,
                    ActivityId = 11104,
                    OpenStageGroupIdLists = new uint[] { 13, 14, 15, 16 }
                }
            };
            foreach (uint groupId in Rsp.ThemeWantedActivity.OpenStageGroupIdLists)
            {
                SingleWantedStageGroupExcel? groupData = SingleWantedStageGroup.GetInstance().FromGroupId((int)groupId);
                if(groupData is not null)
                {
                    Rsp.ThemeWantedActivity.StageGroupInfoLists.Add(new()
                    {
                        StageGroupId = groupId,
                        Progress = (uint)groupData.StageIdList.Length,
                        UnlockMpProgressLists = groupData.MpStageIdList.Select((mpStage, index) => (uint)index + 1).ToArray()
                    });
                }
            }

            session.Send(Packet.FromProto(Rsp, CmdId.GetThemeWantedRsp));
        }
    }
}
