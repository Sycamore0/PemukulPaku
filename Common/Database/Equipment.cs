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

        public Resources.Proto.Material AddMaterial(int materialId, int num = 1)
        {
            int MaterialIndex = Array.FindIndex(MaterialList, material => material.Id == materialId);

            if(MaterialIndex == -1)
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
