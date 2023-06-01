using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands
{
    [CommandHandler("avatar", "Add avatar to player account", CommandType.Player)]
    internal class AvatarCommand : Command
    {
        public override void Run(Session session, string[] args)
        {
            int avatarId = int.Parse(args[1]);
            Run(session.Player, args);

            session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq() { }, CmdId.GetEquipmentDataReq));
            if (avatarId == -1)
            {
                session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq() { AvatarIdLists = new uint[] { 0 } }, CmdId.GetAvatarDataReq));
            }
            else
            {
                session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq() { AvatarIdLists = new uint[] { (uint)avatarId } }, CmdId.GetAvatarDataReq));
            }
        }

        public override void Run(Player player, string[] args)
        {
            string action = args[0];
            int avatarId = int.Parse(args[1]);

            switch (action)
            {
                case "add":
                    if (player.Equipment is not null)
                    {
                        if (avatarId == -1)
                        {
                            foreach (AvatarDataExcel avatarData in AvatarData.GetInstance().All)
                            {
                                if (avatarData.AvatarId >= 9000) continue; // Avoid APHO avatars

                                AvatarScheme avatar = Common.Database.Avatar.Create(avatarData.AvatarId, player.User.Uid, player.Equipment);
                                player.AvatarList = player.AvatarList.Append(avatar).ToArray();
                            }
                        }
                        else
                        {
                            AvatarScheme avatar = Common.Database.Avatar.Create(avatarId, player.User.Uid, player.Equipment);
                            player.AvatarList = player.AvatarList.Append(avatar).ToArray();
                        }
                    }
                    player.Equipment.Save();
                    break;
                default:
                    throw new ArgumentException("Unrecognized action");
            }
        }
    }
}
