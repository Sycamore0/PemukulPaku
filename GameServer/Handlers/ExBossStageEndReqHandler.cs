
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.ExBossStageEndReq)]
    internal class ExBossStageEndReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            ExBossStageEndReq Data = packet.GetDecodedBody<ExBossStageEndReq>();

            session.Send(Packet.FromProto(new ExBossStageEndRsp()
            {
                retcode = ExBossStageEndRsp.Retcode.Succ,
                BossId = Data.BossId,
                EndStatus = Data.EndStatus
            }, CmdId.ExBossStageEndRsp));
        }
    }
}
