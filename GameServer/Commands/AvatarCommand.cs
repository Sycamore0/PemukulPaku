using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
using System.Globalization;

namespace PemukulPaku.GameServer.Commands
{
    [CommandHandler("avatar", "<sel> <id> [Prop] [#]", CommandType.Player, "add 406", "modify -1 star 5")]
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

            if (action.ToLower() == "modify" || action.ToLower() == "mod")
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
            string modType = "";
            int value = 0;
            if (args.Length > 3)
            {
                modType = args[2];
                value = int.Parse(args[3]);
            }
            AvatarScheme? avatar = null;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            switch (action.ToLower())
            {
                case "add":
                    if (avatarId == -1)
                    {
                        foreach (AvatarDataExcel avatarData in AvatarData.GetInstance().All)
                        {
                            if (avatarData.AvatarId >= 9000 || avatarData.AvatarId == 316) continue; // Avoid APHO avatars and scuffed 316

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
                case "mod":
                    if (avatarId == -1)
                    {
                        foreach (AvatarScheme av in player.AvatarList)
                        {
                            av.GetType()?.GetProperty(textInfo.ToTitleCase(modType))?.SetValue(av, (uint)value, null);
                            av.Save();
                        }
                    }
                    else
                    {
                        avatar = player.AvatarList.FirstOrDefault(av => av.AvatarId == avatarId);
                        if (avatar is not null)
                        {
                            avatar.GetType()?.GetProperty(textInfo.ToTitleCase(modType))?.SetValue(avatar, (uint)value, null);
                            avatar.Save();
                        }
                        else
                            throw new ArgumentException("Avatar not found");
                    }
                    break;
                /* broken experiment for an all skills cmd
                case "allskills":
                    if (avatarId == -1)
                    {
                        foreach (AvatarScheme av in player.AvatarList)
                        {
                            foreach (AvatarSubSkillDataExcel subSkillData in AvatarSubSkillData.GetInstance().All)
                            {
                                av.LevelUpSkill((uint)subSkillData.AvatarSubSkillId, true);
                            }
                        }
                    }
                    else
                    {
                        avatar = player.AvatarList.FirstOrDefault(av => av.AvatarId == avatarId);
                        if (avatar is not null)
                        {
                            avatar.SkillLists.Clear();
                            foreach (AvatarScheme.AvatarSkill skill in avatar.SkillLists)
                            {
                                avatar.LevelUpSkill(skill.SkillId, true);
                            }
                        }
                        else
                            throw new ArgumentException("Avatar not found");
                    }
                    break;
                */
                default:
                    throw new ArgumentException("Unrecognized action");
            }
        }
    }
}
