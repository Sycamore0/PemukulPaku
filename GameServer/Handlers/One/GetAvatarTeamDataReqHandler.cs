using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetAvatarTeamDataReq)]
    internal class GetAvatarTeamDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetAvatarTeamDataRsp Rsp = new()
            {
                retcode = GetAvatarTeamDataRsp.Retcode.Succ
            };

            Rsp.AvatarTeamLists.AddRange(session.Player.User.AvatarTeamList);
            Rsp.CustomAvatarTeamLists.AddRange(session.Player.User.CustomAvatarTeamList);

            session.Send(Packet.FromProto(Rsp, CmdId.GetAvatarTeamDataRsp));
        }
    }
}
