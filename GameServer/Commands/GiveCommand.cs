using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
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
            uint value = (args.Length > 1 && args[1] is not null) ? uint.Parse(args[1]) : 0;

            switch (action.ToLower())
            {
                case "avatars":
                case "characters":
                case "chars":
                case "valks":
                case "valkyries":
                    foreach (AvatarDataExcel avatarData in AvatarData.GetInstance().All)
                    {
                        if (avatarData.AvatarId >= 9000 || avatarData.AvatarId == 316) continue; // Avoid APHO avatars

                        AvatarScheme avatar = Common.Database.Avatar.Create(avatarData.AvatarId, player.User.Uid, player.Equipment);
                        player.AvatarList = player.AvatarList.Append(avatar).ToArray();
                    }
                    break;
                case "weapons":
                case "weap":
                case "wep":
                    foreach (WeaponDataExcel weaponData in WeaponData.GetInstance().All)
                    {
                        if (weaponData.EvoId == 0)
                        {
                            Weapon weapon = player.Equipment.AddWeapon(weaponData.Id);
                            weapon.Level = value == 0 ? (uint)weaponData.MaxLv : value;
                        }
                    }
                    break;
                case "weapons-all":
                case "weap-all":
                case "wep-all":
                    foreach (WeaponDataExcel weaponData in WeaponData.GetInstance().All)
                    {
                        Weapon weapon = player.Equipment.AddWeapon(weaponData.Id);
                        weapon.Level = value == 0 ? (uint)weaponData.MaxLv : value;
                    }
                    break;
                case "stigmata":
                case "stigs":
                    foreach (StigmataDataExcel stigmataData in StigmataData.GetInstance().All)
                    {
                        if (stigmataData.EvoId == 0)
                        {
                            Stigmata stigmata = player.Equipment.AddStigmata(stigmataData.Id);
                            stigmata.Level = value == 0 ? (uint)stigmataData.MaxLv : value;
                        }
                    }
                    break;
                case "stigmata-all":
                case "stigs-all":
                    foreach (StigmataDataExcel stigmataData in StigmataData.GetInstance().All)
                    {
                        Stigmata stigmata = player.Equipment.AddStigmata(stigmataData.Id);
                        stigmata.Level = value == 0 ? (uint)stigmataData.MaxLv : value;
                    }
                    break;
                case "materials":
                case "matz":
                    foreach (MaterialDataExcel materialData in MaterialData.GetInstance().All)
                    {
                        player.Equipment.AddMaterial(materialData.Id, value == 0 ? 1 : (int)value);
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
