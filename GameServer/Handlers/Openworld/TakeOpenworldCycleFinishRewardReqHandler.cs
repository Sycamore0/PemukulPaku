using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers.Openworld
{
    [PacketCmdId(CmdId.TakeOpenworldCycleFinishRewardReq)]
    internal class TakeOpenworldCycleFinishRewardReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            TakeOpenworldCycleFinishRewardReq Data = packet.GetDecodedBody<TakeOpenworldCycleFinishRewardReq>();
            OpenWorldScheme? ow = session.Player.OpenWorlds.Where(x => x.MapId == Data.MapId).FirstOrDefault();
            if (ow is not null)
            {
                ow.Cycle = OpenWorldCycleData.GetInstance().GetNextCycle(Data.MapId, Data.Cycle);
                ow.HasTakeFinishRewardCycle = OpenWorldCycleData.GetInstance().GetNextCycle(Data.MapId, Data.Cycle);
            }

            session.Send(Packet.FromProto(new TakeOpenworldCycleFinishRewardRsp()
            {
                retcode = TakeOpenworldCycleFinishRewardRsp.Retcode.Succ,
                MapId = Data.MapId,
                Cycle = Data.Cycle
            }, CmdId.TakeOpenworldCycleFinishRewardRsp));
        }
    }
}
