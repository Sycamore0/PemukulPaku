using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetBuffEffectReq)]
    internal class GetBuffEffectReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new GetBuffEffectRsp() { retcode = GetBuffEffectRsp.Retcode.Succ }, CmdId.GetBuffEffectRsp));
        }
    }
}