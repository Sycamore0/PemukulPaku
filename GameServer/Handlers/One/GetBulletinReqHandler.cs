using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetBulletinReq)]
    internal class GetBulletinReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new GetBulletinRsp() { retcode = GetBulletinRsp.Retcode.Succ, IsAll = true }, CmdId.GetBulletinRsp));
        }
    }
}
