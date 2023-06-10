using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Three
{
    [PacketCmdId(CmdId.GetTeamListReq)]
    internal class GetTeamListReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetTeamListReq Data = packet.GetDecodedBody<GetTeamListReq>();
            GetTeamListRsp Rsp = new() { retcode = GetTeamListRsp.Retcode.Succ };

            session.Send(Packet.FromProto(Rsp, CmdId.GetTeamListRsp));
        }
    }
}
