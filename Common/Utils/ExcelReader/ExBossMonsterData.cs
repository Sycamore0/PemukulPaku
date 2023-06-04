using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class ExBossMonsterData : BaseExcelReader<ExBossMonsterData, ExBossMonsterDataExcel>
    {
        public override string FileName { get { return "ExBossMonsterData.json"; } }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class ExBossMonsterDataExcel
    {
        [JsonProperty("BossGroupId")]
        public int BossGroupId { get; set; }

        [JsonProperty("BossName")]
        public string BossName { get; set; }

        [JsonProperty("BossPrefabPath")]
        public string BossPrefabPath { get; set; }

        [JsonProperty("MonsterId")]
        public int MonsterId { get; set; }

        [JsonProperty("HardLevel")]
        public int HardLevel { get; set; }

        [JsonProperty("HardLevelGroup")]
        public int HardLevelGroup { get; set; }

        [JsonProperty("MonsterHp")]
        public int MonsterHp { get; set; }

        [JsonProperty("MonsterLevel")]
        public uint MonsterLevel { get; set; }

        [JsonProperty("MonsterBaseScore")]
        public int MonsterBaseScore { get; set; }

        [JsonProperty("SceneName")]
        public string SceneName { get; set; }

        [JsonProperty("BossAttribute")]
        public int BossAttribute { get; set; }

        [JsonProperty("BossSkillTipsList")]
        public int[] BossSkillTipsList { get; set; }

        [JsonProperty("DefaultShowSkillDetail")]
        public bool DefaultShowSkillDetail { get; set; }

        [JsonProperty("BossDesc")]
        public HashName BossDesc { get; set; }

        [JsonProperty("ImagePath")]
        public string ImagePath { get; set; }

        [JsonProperty("RestrictList")]
        public int[] RestrictList { get; set; }

        [JsonProperty("EventMark")]
        public HashName EventMark { get; set; }

        [JsonProperty("TimesScore")]
        public int TimesScore { get; set; }

        [JsonProperty("CornerMarkPath")]
        public string CornerMarkPath { get; set; }

        [JsonProperty("UpTagList")]
        public TagList[] UpTagList { get; set; }

        [JsonProperty("DownTagList")]
        public TagList[] DownTagList { get; set; }

        [JsonProperty("ExtraTimeScore")]
        public int ExtraTimeScore { get; set; }

        [JsonProperty("ConfigID")]
        public int ConfigId { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("BossId")]
        public int BossId { get; set; }
    }
}
