using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.UpdateAvatarTeamNotify)]
    internal class UpdateAvatarTeamNotifyHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            UpdateAvatarTeamNotify Data = packet.GetDecodedBody<UpdateAvatarTeamNotify>();

            AvatarTeam? avatarTeam = session.Player.User.AvatarTeamList.FirstOrDefault(team => team.StageType == Data.Team.StageType);
            if(avatarTeam is not null)
            {
                avatarTeam.AvatarIdLists = Data.Team.AvatarIdLists;
            }
            else
            {
                session.Player.User.AvatarTeamList.Add(Data.Team);
            }
        }
    }
}
