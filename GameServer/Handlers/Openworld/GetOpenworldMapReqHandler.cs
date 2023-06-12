using Common;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld
{
    [PacketCmdId(CmdId.GetOpenworldMapReq)]
    internal class GetOpenworldMapReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetOpenworldMapReq Data = packet.GetDecodedBody<GetOpenworldMapReq>();
            OpenWorldScheme? OpenWorldData = session.Player.OpenWorlds.FirstOrDefault(x => x.MapId == Data.MapId);
            if (OpenWorldData is null)
                return;
            GetOpenworldMapRsp Rsp = new()
            {
                retcode = GetOpenworldMapRsp.Retcode.Succ,
                MapId = Data.MapId,
                Cycle = OpenWorldData.Cycle,
                EventRandomSeed = Global.GetRandomSeed(),
                SpawnPoint = OpenWorldData.SpawnPoint,
                Status = 3,
                QuestData = new()
                {
                    IsOpen = false,
                    DayOpenTimes = 0,
                    RefreshLeftTimes = 0,
                    NextRefreshCost = 0,
                    IsCanAbandon = true,
                    ChallengeScore = 0,
                    IsQuestFinish = false,
                    OpenQuestTime = 0
                },
                TechData = new()
                {
                    MapId = Data.MapId
                },
                HasTakeFinishRewardCycle = OpenWorldData.HasTakeFinishRewardCycle
            };

            session.Send(Packet.FromProto(Rsp, CmdId.GetOpenworldMapRsp));
        }
    }
}
