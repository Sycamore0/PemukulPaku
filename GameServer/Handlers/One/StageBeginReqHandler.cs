using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.StageBeginReq)]
    internal class StageBeginReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            StageBeginReq Data = packet.GetDecodedBody<StageBeginReq>();

            StageBeginRsp Rsp = new()
            {
                retcode = StageBeginRsp.Retcode.Succ,
                StageId = Data.StageId,
                Progress = 0,
                IsCollectCheatData = false
            };

            session.Send(Packet.FromProto(Rsp, CmdId.StageBeginRsp));
        }
    }
}
