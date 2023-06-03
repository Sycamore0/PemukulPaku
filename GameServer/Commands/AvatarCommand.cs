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
            string action = args[0];
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

            if (action == "modify")
            {
                List<uint> updatedAvatars = new();
                
                if (avatarId == -1)
                {
                    foreach (AvatarScheme av in session.Player.AvatarList)
                    {
                        updatedAvatars.Add(av.AvatarId);
                    }
                }
                else
                {
                    AvatarScheme? avatar = session.Player.AvatarList.FirstOrDefault(av => av.AvatarId == avatarId);
                    if (avatar is not null)
                    {
                        updatedAvatars.Add(avatar.AvatarId);
                    }
                }

                session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq() { AvatarIdLists = updatedAvatars.ToArray() }, CmdId.GetAvatarDataReq));
            }
        }

        public override void Run(Player player, string[] args)
        {
            string action = args[0];
            int avatarId = int.Parse(args[1]);
            string modType = args[2];
            int value = int.Parse(args[3]);
            AvatarScheme? avatar;

            switch (action)
            {
                case "add":
                    if (avatarId == -1)
                    {
                        foreach (AvatarDataExcel avatarData in AvatarData.GetInstance().All)
                        {
                            if (avatarData.AvatarId >= 9000) continue; // Avoid APHO avatars

                            avatar = Common.Database.Avatar.Create(avatarData.AvatarId, player.User.Uid, player.Equipment);
                            player.AvatarList = player.AvatarList.Append(avatar).ToArray();
                        }
                    }
                    else
                    {
                        avatar = Common.Database.Avatar.Create(avatarId, player.User.Uid, player.Equipment);
                        player.AvatarList = player.AvatarList.Append(avatar).ToArray();
                    }
                    player.Equipment.Save();
                    break;
                case "modify":
                    if (avatarId == -1)
                    {
                        foreach (var av in player.AvatarList)
                        {
                            av.GetType()?.GetProperty(modType)?.SetValue(av, (uint)value, null);
                            av.Save();
                        }
                    }
                    else
                    {
                        avatar = player.AvatarList.FirstOrDefault(av => av.AvatarId == avatarId);
                        if (avatar is not null)
                        {
                            avatar.GetType()?.GetProperty(modType)?.SetValue(avatar, (uint)value, null);
                            avatar.Save();
                        }
                        else
                            c.Error("Invalid AvatarScheme in avatar modify command");
                    }
                    break;
                default:
                    throw new ArgumentException("Unrecognized action");
            }
        }
    }
}
