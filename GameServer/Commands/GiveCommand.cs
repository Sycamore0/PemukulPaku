using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands
{
    [CommandHandler("give", "<sel> [#] [id]", CommandType.Player,
        //example commands list
        "gold 123456789", "stigs 1", "weaps", "mats 999 2008", "valks", "outfits"
    )]
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
            int? value = (args.Length >= 2 && args[1] is not null) ? int.Parse(args[1]) : null;

            switch (action)
            {
                case "avatars":
                case "characters":
                case "chars":
                case "valks":
                case "valkyries":
                    foreach (AvatarDataExcel avatarData in AvatarData.GetInstance().All)
                    {
                        if (avatarData.AvatarId >= 9000 || avatarData.AvatarId == 316) continue; // Avoid scuffed characters

                        AvatarScheme avatar = Common.Database.Avatar.Create(avatarData.AvatarId, player.User.Uid, player.Equipment);
                        player.AvatarList = player.AvatarList.Append(avatar).ToArray();
                    }
                    break;
                case "avatars-scuffed":
                case "valkyries-scuffed":
                    foreach (AvatarDataExcel avatarData in AvatarData.GetInstance().All)
                    {
                        if (!(avatarData.AvatarId >= 9000 || avatarData.AvatarId == 316)) continue; // Adds scuffed characters

                        AvatarScheme avatar = Common.Database.Avatar.Create(avatarData.AvatarId, player.User.Uid, player.Equipment);
                        player.AvatarList = player.AvatarList.Append(avatar).ToArray();
                    }
                    break;
                case "weapons":
                case "weaps":
                    foreach (WeaponDataExcel weaponData in WeaponData.GetInstance().All)
                    {
                        if (weaponData.EvoId == 0)
                        {
                            Weapon weapon = player.Equipment.AddWeapon(weaponData.Id);
                            weapon.Level = value is not null && value <= 0 ? (uint)weaponData.MaxLv : value is not null ? (uint)value : (uint)weaponData.MaxLv;
                        }
                    }
                    break;
                case "weapons-all":
                case "weaps-all":
                    foreach (WeaponDataExcel weaponData in WeaponData.GetInstance().All)
                    {
                        Weapon weapon = player.Equipment.AddWeapon(weaponData.Id);
                        weapon.Level = value is not null && value <= 0 ? (uint)weaponData.MaxLv : value is not null ? (uint)value : (uint)weaponData.MaxLv;
                    }
                    break;
                case "stigmata":
                case "stigs":
                    foreach (StigmataDataExcel stigmataData in StigmataData.GetInstance().All)
                    {
                        if (stigmataData.EvoId == 0)
                        {
                            Stigmata stigmata = player.Equipment.AddStigmata(stigmataData.Id);
                            stigmata.Level = value is not null && value <= 0 ? (uint)stigmataData.MaxLv : value is not null ? (uint)value : (uint)stigmataData.MaxLv;
                        }
                    }
                    break;
                case "stigmata-all":
                case "stigs-all":
                    foreach (StigmataDataExcel stigmataData in StigmataData.GetInstance().All)
                    {
                        Stigmata stigmata = player.Equipment.AddStigmata(stigmataData.Id);
                        stigmata.Level = value is not null && value <= 0 ? (uint)stigmataData.MaxLv : value is not null ? (uint)value : (uint)stigmataData.MaxLv;
                    }
                    break;
                case "materials":
                case "mats":
                case "matz":
                    foreach (MaterialDataExcel materialData in MaterialData.GetInstance().All)
                    {
                        player.Equipment.AddMaterial(materialData.Id, value is not null && value != 0 ? (int)value : materialData.QuantityLimit);
                    }
                    break;
                case "material-id":
                case "gold":
                    int materialId = args.Length >= 3 && args[2] is not null ? int.Parse(args[2]) : 100;
                    int? quantityLimit = MaterialData.GetInstance().All.FirstOrDefault(m => m.Id == materialId)?.QuantityLimit;
                    if (quantityLimit is not null)
                    {
                        player.Equipment.AddMaterial(materialId, value is not null && value != 0 ? (int)value : (int)quantityLimit);
                    }
                    break;
                case "dress":
                case "outfits":
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
