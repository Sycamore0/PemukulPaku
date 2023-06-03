using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class EquipmentLevelData : BaseExcelReader<EquipmentLevelData, EquipmentLevelDataExcel>
    {
        public override string FileName { get { return "EquipmentLevelData.json"; } }

        public EquipmentLevelDataExcel? FromLevel(int level)
        {
            return All.Where(levelData => levelData.Level == level).FirstOrDefault();
        }

        public PlayerLevelData.LevelData CalculateLevel(int level, int exp, int expType)
        {
            int expRemain = exp;

            foreach (EquipmentLevelDataExcel levelData in All.Where(levelData => levelData.Level >= level))
            {
                if (expRemain < 1)
                {
                    break;
                }
                else if (expRemain >= levelData.Type1[expType])
                {
                    if (level == All.OrderByDescending(level => level.Level).First().Level)
                    {
                        expRemain = All.OrderByDescending(level => level.Level).First().Type1[expType];
                        break;
                    }

                    level++;
                    expRemain -= levelData.Type1[expType];
                }
            }

            return new PlayerLevelData.LevelData(level, expRemain);
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class EquipmentLevelDataExcel
    {
        [JsonProperty("Type1")]
        public int[] Type1 { get; set; }

        [JsonProperty("weaponUpgradeCost")]
        public int WeaponUpgradeCost { get; set; }

        [JsonProperty("weaponEvoCost")]
        public int WeaponEvoCost { get; set; }

        [JsonProperty("stigmataUpgradeCost")]
        public int StigmataUpgradeCost { get; set; }

        [JsonProperty("stigmataEvoCost")]
        public int StigmataEvoCost { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }
    }
}
