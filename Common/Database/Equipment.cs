using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Database
{
    public class Equipment
    {
        public static readonly IMongoCollection<EquipmentScheme> collection = Global.db.GetCollection<EquipmentScheme>("Equipments");

        public static EquipmentScheme FromUid(uint uid)
        {
            return collection.AsQueryable().Where(collection => collection.OwnerUid == uid).FirstOrDefault() ?? Create(uid);
        }

        public static EquipmentScheme Create(uint uid)
        {
            EquipmentScheme? tryEquipment = collection.AsQueryable().Where(collection => collection.OwnerUid == uid).FirstOrDefault();
            if (tryEquipment != null) { return tryEquipment; }

            EquipmentScheme Equipment = new()
            {
                OwnerUid = uid,
                MaterialList = new Resources.Proto.Material[] { new Resources.Proto.Material { Id = 100, Num = 750 }, new Resources.Proto.Material { Id = 119107, Num = 6 } },
                WeaponList = Array.Empty<Weapon>(),
                StigmataList = Array.Empty<Stigmata>(),
                MechaList = Array.Empty<Mecha>(),
            };

            collection.InsertOne(Equipment);

            return Equipment;
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class EquipmentScheme
    {
        public ObjectId Id { get; set; }
        public uint OwnerUid { get; set; }
        public Resources.Proto.Material[] MaterialList { get; set; }
        public Mecha[] MechaList { get; set; }
        public Stigmata[] StigmataList { get; set; }
        public Weapon[] WeaponList { get; set; }

        public Weapon AddWeapon(int weaponId)
        {
            WeaponDataExcel? weaponData = WeaponData.GetInstance().FromId(weaponId);
            if (weaponData == null) { throw new ArgumentException("Invalid weaponId"); }

            Weapon weapon = new()
            {
                UniqueId = (uint)AutoIncrement.GetNextNumber("Weapon", 100),
                Id = (uint)weaponData.Id,
                Level = 1,
                Exp = 0,
                IsExtracted = false,
                IsProtected = false
            };

            WeaponList = WeaponList.Append(weapon).ToArray();
            return weapon;
        }

        public Stigmata AddStigmata(int stigmataId)
        {
            StigmataDataExcel? stigmataData = StigmataData.GetInstance().FromId(stigmataId);
            if (stigmataData == null) { throw new ArgumentException("Invalid stigmataId"); }

            Stigmata stigmata = new()
            {
                UniqueId = (uint)AutoIncrement.GetNextNumber("Stigmata", 100),
                Id = (uint)stigmataData.Id,
                Level = 1,
                Exp = 0,
                IsProtected = false,
                SlotNum = 0,
                RefineValue = 0,
                PromoteTimes = 0
            };

            StigmataList = StigmataList.Append(stigmata).ToArray();
            return stigmata;
        }

        public Resources.Proto.Material[] AddWeaponExpByConsumeItem(uint uniqueId, List<EquipmentItem> consumeItemList)
        {
            int expAdd = consumeItemList.Select(item =>
            {
                MaterialDataExcel? materialData = MaterialData.GetInstance().FromId(item.IdOrUniqueId);
                if (materialData == null)
                    return 0;

                int MaterialIndex = MaterialList.ToList().FindIndex(mat => mat.Id == item.IdOrUniqueId);
                MaterialList[MaterialIndex].Num -= item.Num;

                return materialData.GearExpProvideBase * (int)item.Num;
            }).Sum();
            AddWeaponExp(uniqueId, expAdd);

            return MaterialList.Where(material => consumeItemList.Select(item => item.IdOrUniqueId).ToArray().Contains(material.Id)).ToArray();
        }

        public void AddWeaponExp(uint uniqueId, int exp)
        {
            int weaponIndex = WeaponList.ToList().FindIndex(weapon => weapon.UniqueId == uniqueId);
            if (weaponIndex == -1) return;
            WeaponDataExcel? weaponData = WeaponData.GetInstance().FromId((int)WeaponList[weaponIndex].Id);
            if (weaponData is null) return;

            PlayerLevelData.LevelData levelData = EquipmentLevelData.GetInstance().CalculateLevel((int)WeaponList[weaponIndex].Level, (int)WeaponList[weaponIndex].Exp + exp, weaponData.ExpType);

            if (levelData.Level > weaponData.MaxLv)
            {
                levelData.Level = weaponData.MaxLv;
                EquipmentLevelDataExcel? LevelData = EquipmentLevelData.GetInstance().FromLevel(levelData.Level);
                if (LevelData is null) return;

                levelData.Exp = LevelData.Type1[weaponData.ExpType];
            }

            WeaponList[weaponIndex].Level = (uint)levelData.Level;
            WeaponList[weaponIndex].Exp = (uint)levelData.Exp;
        }

        public Resources.Proto.Material[] AddStigmataExpByConsumeItem(uint uniqueId, List<EquipmentItem> consumeItemList)
        {
            int expAdd = consumeItemList.Select(item =>
            {
                MaterialDataExcel? materialData = MaterialData.GetInstance().FromId(item.IdOrUniqueId);
                if (materialData == null)
                    return 0;

                int MaterialIndex = MaterialList.ToList().FindIndex(mat => mat.Id == item.IdOrUniqueId);
                MaterialList[MaterialIndex].Num -= item.Num;

                return materialData.GearExpProvideBase * (int)item.Num;
            }).Sum();
            AddStigmataExp(uniqueId, expAdd);

            return MaterialList.Where(material => consumeItemList.Select(item => item.IdOrUniqueId).ToArray().Contains(material.Id)).ToArray();
        }

        public void AddStigmataExp(uint uniqueId, int exp)
        {
            int stigmataIndex = StigmataList.ToList().FindIndex(stigmata => stigmata.UniqueId == uniqueId);
            if (stigmataIndex == -1) return;
            StigmataDataExcel? stigmataData = StigmataData.GetInstance().FromId((int)StigmataList[stigmataIndex].Id);
            if (stigmataData is null) return;

            PlayerLevelData.LevelData levelData = EquipmentLevelData.GetInstance().CalculateLevel((int)StigmataList[stigmataIndex].Level, (int)StigmataList[stigmataIndex].Exp + exp, stigmataData.ExpType);

            if (levelData.Level > stigmataData.MaxLv)
            {
                levelData.Level = stigmataData.MaxLv;
                EquipmentLevelDataExcel? LevelData = EquipmentLevelData.GetInstance().FromLevel(levelData.Level);
                if (LevelData is null) return;

                levelData.Exp = LevelData.Type1[stigmataData.ExpType];
            }

            StigmataList[stigmataIndex].Level = (uint)levelData.Level;
            StigmataList[stigmataIndex].Exp = (uint)levelData.Exp;
        }

        public Resources.Proto.Material AddMaterial(int materialId, int num = 1)
        {
            int MaterialIndex = Array.FindIndex(MaterialList, material => material.Id == materialId);

            if (MaterialIndex == -1)
            {
                MaterialList = MaterialList.Append(new() { Id = (uint)materialId, Num = num < 0 ? 0 : (uint)num }).ToArray();
            }
            else
            {
                if (num < 0)
                {
                    MaterialList[MaterialIndex].Num -= (uint)Math.Abs(num);
                }
                else
                {
                    MaterialList[MaterialIndex].Num += (uint)num;
                }
            }

            return MaterialList.Where(material => material.Id == materialId).First();
        }

        public void Save()
        {
            Equipment.collection.ReplaceOne(Builders<EquipmentScheme>.Filter.Eq(equipment => equipment.Id, Id), this);
        }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
