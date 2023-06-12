using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.One
{
    [PacketCmdId(CmdId.MpGetTeamReq)]
    internal class MpGetTeamReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.Send(Packet.FromProto(new MpGetTeamRsp() { retcode = MpGetTeamRsp.Retcode.NotInTeam }, CmdId.MpGetTeamRsp));
        }
    }
}
