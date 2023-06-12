using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld
{
    [PacketCmdId(CmdId.OpenworldStageBeginReq)]
    internal class OpenworldStageBeginReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            OpenworldStageBeginReq Data = packet.GetDecodedBody<OpenworldStageBeginReq>();

            session.Send(Packet.FromProto(new OpenworldStageBeginRsp()
            {
                retcode = OpenworldStageBeginRsp.Retcode.Succ,
                MapId = Data.MapId
            }, CmdId.OpenworldStageBeginRsp));
        }
    }
}
