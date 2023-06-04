using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetGodWarReq)]
    internal class GetGodWarReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new GetGodWarRsp() { retcode = GetGodWarRsp.Retcode.NotOpen }, CmdId.GetGodWarRsp));
        }
    }
}
