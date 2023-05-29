using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetGobackReq)]
    internal class GetGobackReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new GetGobackRsp() { retcode = GetGobackRsp.Retcode.Succ }, CmdId.GetGobackRsp));
        }
    }
}
