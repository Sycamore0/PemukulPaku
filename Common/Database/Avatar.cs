using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Database
{
    public class Avatar
    {
        public static readonly IMongoCollection<AvatarScheme> collection = Global.db.GetCollection<AvatarScheme>("Avatars");

        public static AvatarScheme[] AvatarsFromUid(uint uid)
        {
            return collection.AsQueryable().Where(collection => collection.OwnerUid == uid).ToArray();
        }

        public static AvatarScheme Create(int avatarId, uint uid, EquipmentScheme equipment)
        {
            AvatarScheme? tryAvatar = collection.AsQueryable().Where(collection => collection.AvatarId == avatarId && collection.OwnerUid == uid).FirstOrDefault();
            if (tryAvatar != null) { return tryAvatar; }

            AvatarDataExcel? avatarData = AvatarData.GetInstance().FromId(avatarId);
            if(avatarData == null) { throw new ArgumentException("Invalid avatarId"); }

            Weapon weapon = equipment.AddWeapon(avatarData.InitialWeapon);

            AvatarScheme avatar = new()
            {
                OwnerUid = uid,
                AvatarId = (uint)avatarData.AvatarId,
                DressId = (uint)avatarData.DefaultDressId,
                DressLists = new[] { (uint)avatarData.DefaultDressId },
                Exp = 0,
                Fragment = 0,
                Level = 1,
                Star = (uint)avatarData.UnlockStar,
                StigmataUniqueId1 = 0,
                StigmataUniqueId2 = 0,
                StigmataUniqueId3 = 0,
                SubStar = 0,
                TouchGoodfeel = 0,
                TodayHasAddGoodfeel = 0,
                WeaponUniqueId = weapon.UniqueId,
                SkillLists = new()
            };

            if(avatarData.AvatarId == 101)
            {
                Stigmata defaultStigmata1 = equipment.AddStigmata(30007);
                Stigmata defaultStigmata2 = equipment.AddStigmata(30060);
                Stigmata defaultStigmata3 = equipment.AddStigmata(30113);

                avatar.StigmataUniqueId1 = defaultStigmata1.UniqueId;
                avatar.StigmataUniqueId2 = defaultStigmata2.UniqueId;
                avatar.StigmataUniqueId3 = defaultStigmata3.UniqueId;
            }

            avatar.SkillLists.AddRange(avatarData.SkillList.Select(skillId => new AvatarScheme.AvatarSkill { SkillId = (uint)skillId }));

            collection.InsertOne(avatar);

            return avatar;
        }
    }

    public class AvatarScheme : Resources.Proto.Avatar
    {
        public class AvatarSkill : Resources.Proto.AvatarSkill
        {
            public new List<AvatarSubSkill> SubSkillLists { get; set; } = new();
        }

        public ObjectId Id { get; set; }
        public uint OwnerUid { get; set; }
        public new List<AvatarSkill> SkillLists { get; set; } = new();


        public void Save()
        {
            Avatar.collection.ReplaceOne(Builders<AvatarScheme>.Filter.Eq(avatar => avatar.Id, Id), this);
        }

        public PlayerLevelData.LevelData AddExp(uint exp)
        {
            PlayerLevelData.LevelData levelData = AvatarLevelData.GetInstance().CalculateLevel((int)Level, (int)(exp + Exp));
            Level = (uint)levelData.Level;
            Exp = (uint)levelData.Exp;

            return levelData;
        }

        public void StarUp()
        {
            AvatarDataExcel? avatarData = AvatarData.GetInstance().FromId((int)AvatarId);
            if (avatarData is not null)
            {
                AvatarStarType.StarInfo nextStarInfo = AvatarStarType.GetInstance().GetNextStar(avatarData.AvatarType, avatarData.AvatarStarUpType, new((int)Star, (int)SubStar));
                SubStar = (uint)nextStarInfo.SubStar;
                Star = (uint)nextStarInfo.Star;
                Fragment -= (uint)nextStarInfo.Cost;
            }
        }

        public AvatarDetailData ToDetailData(EquipmentScheme equipment)
        {
            Weapon? weapon = equipment.WeaponList.Where(x => x.UniqueId == WeaponUniqueId).FirstOrDefault();
            Stigmata? stigmata1 = equipment.StigmataList.Where(x => x.UniqueId == StigmataUniqueId1).FirstOrDefault();
            Stigmata? stigmata2 = equipment.StigmataList.Where(x => x.UniqueId == StigmataUniqueId2).FirstOrDefault();
            Stigmata? stigmata3 = equipment.StigmataList.Where(x => x.UniqueId == StigmataUniqueId3).FirstOrDefault();

            AvatarDetailData detailData = new()
            {
                AvatarId = AvatarId,
                AvatarArtifact = AvatarArtifact,
                AvatarLevel = Level,
                AvatarStar = Star,
                AvatarSubStar = SubStar,
                DressId = DressId,
            };

            if (weapon is not null)
            {
                detailData.Weapon = new()
                {
                    Id = weapon.Id,
                    Level = weapon.Level,
                    UniqueId = weapon.UniqueId,
                    SubWeaponId = weapon.SubWeaponId
                };
            }

            if (stigmata1 is not null)
            {
                detailData.Stigmata1 = new()
                {
                    Id = stigmata1.Id,
                    Level = stigmata1.Level,
                    UniqueId = stigmata1.UniqueId
                };
                detailData.Stigmata1.RuneLists.AddRange(stigmata1.RuneLists);
            }

            if (stigmata2 is not null)
            {
                detailData.Stigmata2 = new()
                {
                    Id = stigmata2.Id,
                    Level = stigmata2.Level,
                    UniqueId = stigmata2.UniqueId
                };
                detailData.Stigmata2.RuneLists.AddRange(stigmata2.RuneLists);
            }

            if (stigmata3 is not null)
            {
                detailData.Stigmata3 = new()
                {
                    Id = stigmata3.Id,
                    Level = stigmata3.Level,
                    UniqueId = stigmata3.UniqueId
                };
                detailData.Stigmata3.RuneLists.AddRange(stigmata3.RuneLists);
            }

            detailData.SkillLists.AddRange(SkillLists.Select(x =>
            {
                AvatarSkillDetailData skillDetailData = new() { SkillId = x.SkillId };
                skillDetailData.SubSkillLists.AddRange(x.SubSkillLists.Select(x => new AvatarSubSkillDetailData() { IsMask = x.IsMask, Level = x.Level, SubSkillId = x.SubSkillId }));
                return skillDetailData;
            }));

            return detailData;
        }

        public void AddFragment(uint num) { Fragment += num; }

        public void SetDress(uint dressId) { DressId = dressId; }

        public void LevelUpSkill(uint subSkillId, bool isLevelUpAll = false)
        {
            AvatarSubSkillDataExcel? subSkillData = AvatarSubSkillData.GetInstance().FromId((int)subSkillId);
            if (subSkillData is null)
                return;

            AvatarSkill? avatarSkill = SkillLists.Where(skill => skill.SkillId == subSkillData.SkillId).FirstOrDefault();
            if(avatarSkill is not null)
            {
                AvatarSubSkill? avatarSubSkill = avatarSkill.SubSkillLists.FirstOrDefault(skill => skill.SubSkillId == subSkillData.AvatarSubSkillId);
                if (avatarSubSkill is not null)
                {
                    if (isLevelUpAll)
                    {
                        avatarSubSkill.Level = (uint)subSkillData.MaxLv;
                    }
                    else
                    {
                        if (avatarSubSkill.Level < subSkillData.MaxLv)
                            avatarSubSkill.Level++;
                    }
                }
                else
                {
                    avatarSkill.SubSkillLists.Add(new() { SubSkillId = (uint)subSkillData.AvatarSubSkillId, Level = 1, IsMask = false });
                }
            }
            else
            {
                AvatarSkill newAvatarSkill = new() { SkillId = (uint)subSkillData.SkillId };
                newAvatarSkill.SubSkillLists.Add(new() { SubSkillId = (uint)subSkillData.AvatarSubSkillId, Level = 1, IsMask = false });
                SkillLists.Add(newAvatarSkill);
            }
        }
    }
}
