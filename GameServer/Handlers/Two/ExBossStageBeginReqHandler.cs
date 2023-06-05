using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.ExBossStageBeginReq)]
    internal class ExBossStageBeginReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new ExBossStageBeginRsp() { retcode = ExBossStageBeginRsp.Retcode.Succ }, CmdId.ExBossStageBeginRsp));
        }
    }
}
