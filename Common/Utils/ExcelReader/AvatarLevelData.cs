using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class AvatarLevelData : BaseExcelReader<AvatarLevelData, AvatarLevelDataExcel>
    {
        public override string FileName { get { return "AvatarLevelData.json"; } }

        public PlayerLevelData.LevelData CalculateLevel(int level, int exp)
        {
            int expRemain = exp;

            foreach (AvatarLevelDataExcel levelData in All.Where(levelData => levelData.Level >= level))
            {
                if (expRemain < 1)
                {
                    break;
                }
                else if (expRemain >= levelData.Exp)
                {
                    if (level == All.OrderByDescending(level => level.Level).First().Level) 
                    {
                        expRemain = All.OrderByDescending(level => level.Level).First().Exp;
                        break;
                    }

                    level++;
                    expRemain -= levelData.Exp;
                }
            }

            return new PlayerLevelData.LevelData(level, expRemain);
        }
        
        public int CalculateCost(int startLevel, int endLevel)
        {
            int[] costs = All.Where(level => level.Level > startLevel && level.Level < endLevel).Select(level => level.Cost).ToArray();
            return costs.Sum();
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class AvatarLevelDataExcel
    {
        [JsonProperty("exp")]
        public int Exp { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("avatarAssistConf")]
        public double AvatarAssistConf { get; set; }

        [JsonProperty("subSkillScoin")]
        public int SubSkillScoin { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }
    }
}
