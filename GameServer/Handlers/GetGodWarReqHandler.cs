using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetGodWarReq)]
    internal class GetGodWarReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetGodWarRsp Rsp = new() { retcode = GetGodWarRsp.Retcode.Succ };

            session.Send(Packet.FromProto(Rsp, CmdId.GetGodWarRsp));
        }
    }
}
