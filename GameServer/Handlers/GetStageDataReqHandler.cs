using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetStageDataReq)]
    internal class GetStageDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetStageDataRsp Rsp = new()
            {
                retcode = GetStageDataRsp.Retcode.Succ,
                IsAll = true,
            };

            Rsp.StageLists.AddRange(StageData.GetInstance().All.Select(stage => new Stage()
            {
                Id = (uint)stage.LevelId,
                IsDone = true,
                Progress = (uint)stage.MaxProgress,
                EnterTimes = 1
            }));

            session.Send(Packet.FromProto(Rsp, CmdId.GetStageDataRsp));
        }
    }
}
