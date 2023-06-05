using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetMissionDataReq)]
    internal class GetMissionDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetMissionDataRsp Rsp = new()
            {
                retcode = GetMissionDataRsp.Retcode.Succ,
                IsAll = true
            };

            MissionDataExcel[] missionDatas = MissionData.GetInstance().All;

            Rsp.MissionLists.AddRange(missionDatas.Select(mission => new Mission()
            {
                MissionId = (uint)mission.Id,
                Status = MissionStatus.MissionFinish,
                Priority = (uint)mission.Priority,
                Progress = (uint)mission.TotalProgress,
                BeginTime = session.Player.User.GetCreationTime(),
                EndTime = 2073239999,
                CycleId = 1
            }));

            session.Send(Packet.FromProto(Rsp, CmdId.GetMissionDataRsp));
        }
    }
}
