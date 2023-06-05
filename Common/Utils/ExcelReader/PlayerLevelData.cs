using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class PlayerLevelData : BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>
    {
        public override string FileName { get { return "PlayerLevelData.json"; } }
        public PlayerLevelDataExcel? FromLevel(int level)
        {
            return All.Where(levelData => levelData.Level == level).FirstOrDefault();
        }

        public struct LevelData
        {
            public LevelData(int level, int exp)
            {
                Level = level;
                Exp = exp;
            }

            public int Level { get; set; }
            public int Exp { get; set; }
        }

        public LevelData CalculateLevel(int exp)
        {
            int level = 1;
            int expRemain = exp;

            foreach (PlayerLevelDataExcel levelData in All)
            {
                if(expRemain < 1)
                {
                    break;
                }
                else if(expRemain >= levelData.Exp)
                {
                    level++;
                    expRemain -= levelData.Exp;
                }
            }

            return new LevelData(level, expRemain);
        }

        public uint ExBossRankFromExp(int exp)
        {
            int UserLevel = CalculateLevel(exp).Level;
            if (UserLevel >= 81)
            {
                return 104;
            }
            else if (UserLevel >= 70)
            {
                return 103;
            }
            else if (UserLevel >= 56)
            {
                return 102;
            }
            else
            {
                return 101;
            }
        }

        public LevelData CalculateExpForLevel(int level)
        {
            int exp = 0;

            foreach (PlayerLevelDataExcel levelData in All)
            {
                if (levelData.Level >= level)
                {
                    break;
                }else
                {
                    exp += levelData.Exp;
                    continue;
                }

            }

            return new LevelData(level, exp);
        }

        public int GetMaxPossibleExp()
        {
            return All.Select(x => x.Exp).Sum() - 1;
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class PlayerLevelDataExcel
    {
        [JsonProperty("exp")]
        public int Exp { get; set; }

        [JsonProperty("stamina")]
        public int Stamina { get; set; }

        [JsonProperty("numFriends")]
        public int NumFriends { get; set; }

        [JsonProperty("avatarLevelLimit")]
        public int AvatarLevelLimit { get; set; }

        [JsonProperty("staminaBonus")]
        public int StaminaBonus { get; set; }

        [JsonProperty("sweepStaminaLimit")]
        public int SweepStaminaLimit { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
