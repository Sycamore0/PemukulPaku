using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;

namespace PemukulPaku.GameServer.Handlers.One
{
    [PacketCmdId(CmdId.MpTeamEnterLobbyReq)]
    public class MpTeamEnterLobbyReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            MpTeamEnterLobbyReq Data = packet.GetDecodedBody<MpTeamEnterLobbyReq>();
            Lobby.GetInstance().Teams.TryGetValue(session.Player.User.Uid, out Team? team);
            MpTeamEnterLobbyRsp Rsp = new() { retcode = MpTeamEnterLobbyRsp.Retcode.NotInTeam };
            if (team is not null)
            {
                team.StageId = Data.StageId;
                Lobby.GetInstance().SyncTeam(session.Player.User.Uid);
                Rsp.retcode = MpTeamEnterLobbyRsp.Retcode.Succ;
                Rsp.StageId = team.StageId;
                Rsp.TeamId = team.LeaderUid;
                Rsp.TeamName = team.Name;
            }

            session.Send(Packet.FromProto(Rsp, CmdId.MpTeamEnterLobbyRsp));
        }
    }
}
