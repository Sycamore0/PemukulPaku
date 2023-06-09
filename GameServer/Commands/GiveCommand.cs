using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands
{
    [CommandHandler("give", "<sel> [#] [id]", CommandType.Player,
        //example commands list
        "gold 123456789","stigs 1", "weaps", "mats 999 2008", "valks", "outfits"
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
            int value = (args.Length > 1 && args[1] is not null) ? int.Parse(args[1]) : -1;

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
                            weapon.Level = value <= 0 ? (uint)weaponData.MaxLv : (uint)value;
                        }
                    }
                    break;
                case "weapons-all":
                case "weaps-all":
                    foreach (WeaponDataExcel weaponData in WeaponData.GetInstance().All)
                    {
                        Weapon weapon = player.Equipment.AddWeapon(weaponData.Id);
                        weapon.Level = value <= 0 ? (uint)weaponData.MaxLv : (uint)value;
                    }
                    break;
                case "stigmata":
                case "stigs":
                    foreach (StigmataDataExcel stigmataData in StigmataData.GetInstance().All)
                    {
                        if (stigmataData.EvoId == 0)
                        {
                            Stigmata stigmata = player.Equipment.AddStigmata(stigmataData.Id);
                            stigmata.Level = value <= 0 ? (uint)stigmataData.MaxLv : (uint)value;
                        }
                    }
                    break;
                case "stigmata-all":
                case "stigs-all":
                    foreach (StigmataDataExcel stigmataData in StigmataData.GetInstance().All)
                    {
                        Stigmata stigmata = player.Equipment.AddStigmata(stigmataData.Id);
                        stigmata.Level = value <= 0 ? (uint)stigmataData.MaxLv : (uint)value;
                    }
                    break;
                case "materials":
                case "mats":
                case "matz":
                    foreach (MaterialDataExcel materialData in MaterialData.GetInstance().All)
                    { 
                        player.Equipment.AddMaterial(materialData.Id, value > 0 || value < -1 ? value : 9999);
                    }
                    break;
                case "material-id":
                case "gold":
                    int materialId = args[2] is not null ? int.Parse(args[2]) : 100;
                    player.Equipment.AddMaterial(materialId, value > 0 || value < -1 ? value : 9999);
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
