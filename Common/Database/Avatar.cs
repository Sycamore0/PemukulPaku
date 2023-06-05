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

        public void AddFragment(uint num) { Fragment += num; }

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
