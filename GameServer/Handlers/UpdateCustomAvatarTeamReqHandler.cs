using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.UpdateCustomAvatarTeamReq)]
    internal class UpdateCustomAvatarTeamReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            UpdateCustomAvatarTeamReq Data = packet.GetDecodedBody<UpdateCustomAvatarTeamReq>();
            UpdateCustomAvatarTeamRsp Rsp = new() { retcode = UpdateCustomAvatarTeamRsp.Retcode.Succ };

            CustomAvatarTeam? customAvatarTeam = session.Player.User.CustomAvatarTeamList.FirstOrDefault(x => x.TeamId == Data.Team.TeamId);
            if (customAvatarTeam is not null) 
            {
                customAvatarTeam.AvatarIdLists = Data.Team.AvatarIdLists;
                customAvatarTeam.ElfIdLists = Data.Team.ElfIdLists;
                customAvatarTeam.Name = Data.Team.Name;
            }
            else
            {
                session.Player.User.CustomAvatarTeamList.Add(Data.Team);
            }

            GetAvatarTeamDataRsp avatarTeamDataRsp = new() { retcode = GetAvatarTeamDataRsp.Retcode.Succ };
            avatarTeamDataRsp.CustomAvatarTeamLists.Add(Data.Team);

            session.Send(Packet.FromProto(avatarTeamDataRsp, CmdId.GetAvatarTeamDataRsp), Packet.FromProto(Rsp, CmdId.UpdateCustomAvatarTeamRsp));
        }
    }
}
