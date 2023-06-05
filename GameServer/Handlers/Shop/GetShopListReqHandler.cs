using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetShopListReq)]
    internal class GetShopListReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new GetShopListRsp() { retcode = GetShopListRsp.Retcode.Succ, IsAll = true }, CmdId.GetShopListRsp));
        }
    }
}
