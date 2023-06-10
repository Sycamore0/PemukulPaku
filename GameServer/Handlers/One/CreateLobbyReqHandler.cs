using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;

namespace PemukulPaku.GameServer.Handlers.One
{
    [PacketCmdId(CmdId.CreateLobbyReq)]
    internal class CreateLobbyReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            CreateLobbyReq Data = packet.GetDecodedBody<CreateLobbyReq>();
            Team team = Lobby.GetInstance().CreateTeam(new(Data.StageId, Data.MinLevel, Data.LobbyEnterType, session, Data.TeamName));
            CreateLobbyRsp Rsp = new()
            {
                retcode = CreateLobbyRsp.Retcode.Succ,
                LobbyId = team.LeaderUid,
                StageId = team.StageId,
                MinLevel = team.MinLevel,
                LobbyEnterType = team.LobbyEnterType,
                MaxLevel = 0,
                TeamName = team.Name
            };

            session.Send(Packet.FromProto(Rsp, CmdId.CreateLobbyRsp));
        }
    }
}
