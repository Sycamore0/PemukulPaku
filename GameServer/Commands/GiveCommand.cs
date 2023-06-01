using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using MongoDB.Bson;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands
{
    [CommandHandler("give", "Mass give resources", CommandType.Player)]
    internal class GiveCommand : Command
    {
        public override void Run(Session session, string[] args)
        {
            Run(session.Player, args);

            session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq() { }, CmdId.GetEquipmentDataReq));
            session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq() { AvatarIdLists = new uint[] { 0 } }, CmdId.GetAvatarDataReq));
        }

        public override void Run(Player player, string[] args)
        {
            string action = args[0];

            switch (action)
            {
                case "avatars":
                case "characters":
                case "chars":
                    foreach (AvatarDataExcel avatarData in AvatarData.GetInstance().All)
                    {
                        if (avatarData.AvatarId >= 9000) continue; // Avoid APHO avatars

                        AvatarScheme avatar = Common.Database.Avatar.Create(avatarData.AvatarId, player.User.Uid, player.Equipment);
                        player.AvatarList = player.AvatarList.Append(avatar).ToArray();
                    }
                    break;
                case "weapons":
                    foreach (WeaponDataExcel weaponData in WeaponData.GetInstance().All)
                    {
                        player.Equipment.AddWeapon(weaponData.Id);
                    }
                    break;
                case "stigmata":
                case "stigs":
                    foreach (StigmataDataExcel stigmataData in StigmataData.GetInstance().All)
                    {
                        player.Equipment.AddStigmata(stigmataData.Id);
                    }
                    break;
                case "materials":
                case "matz":
                    foreach (MaterialDataExcel materialData in MaterialData.GetInstance().All)
                    {
                        player.Equipment.AddMaterial(materialData.Id, 9999999);
                    }
                    break;
                case "dress":
                    foreach (DressDataExcel dressData in DressData.GetInstance().All)
                    {
                        foreach (int avatarId in dressData.AvatarIdList)
                        {
                            AvatarScheme? avatar = player.AvatarList.ToList().Find(av => av.AvatarId == avatarId);

                            if (avatar is not null)
                            {
                                avatar.DressLists = avatar.DressLists.Append((uint)dressData.DressId).ToArray();
                                avatar.Save();
                            }
                        }
                    }
                    break;
                default:
                    throw new ArgumentException("Unrecognized action");
            }

            player.User.Save();
        }
    }
}
