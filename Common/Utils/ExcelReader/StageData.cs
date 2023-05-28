using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class StageData : BaseExcelReader<StageData, StageDataExcel>
    {
        public override string FileName { get { return "StageData_Main.json"; } }

        public StageDataExcel? FromId(int id)
        {
            return All.Where(stage => stage.LevelId == id).FirstOrDefault();
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class StageDataExcel
    {
        [JsonProperty("name")]
        public HashName Name { get; set; }

        [JsonProperty("chapterId")]
        public int ChapterId { get; set; }

        [JsonProperty("actId")]
        public int ActId { get; set; }

        [JsonProperty("sectionId")]
        public int SectionId { get; set; }

        [JsonProperty("difficulty")]
        public int Difficulty { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("tag")]
        public int[] Tag { get; set; }

        [JsonProperty("battleType")]
        public int BattleType { get; set; }

        [JsonProperty("enterTimes")]
        public int EnterTimes { get; set; }

        [JsonProperty("resetType")]
        public int ResetType { get; set; }

        [JsonProperty("resetCoinID")]
        public int ResetCoinId { get; set; }

        [JsonProperty("resetCostType")]
        public int ResetCostType { get; set; }

        [JsonProperty("resetTimes")]
        public int ResetTimes { get; set; }

        [JsonProperty("staminaCost")]
        public int StaminaCost { get; set; }

        [JsonProperty("avatarExpReward")]
        public int AvatarExpReward { get; set; }

        [JsonProperty("avatarExpInside")]
        public int AvatarExpInside { get; set; }

        [JsonProperty("scoinReward")]
        public int ScoinReward { get; set; }

        [JsonProperty("scoinInside")]
        public double ScoinInside { get; set; }

        [JsonProperty("maxScoinReward")]
        public int MaxScoinReward { get; set; }

        [JsonProperty("maxProgress")]
        public int MaxProgress { get; set; }

        [JsonProperty("HighlightDisplayDropIdList")]
        public int[] HighlightDisplayDropIdList { get; set; }

        [JsonProperty("dropList")]
        public int[] DropList { get; set; }

        [JsonProperty("recommendPlayerLevel")]
        public int RecommendPlayerLevel { get; set; }

        [JsonProperty("unlockPlayerLevel")]
        public int UnlockPlayerLevel { get; set; }

        [JsonProperty("unlockStarNum")]
        public int UnlockStarNum { get; set; }

        [JsonProperty("preLevelID")]
        public int[] PreLevelId { get; set; }

        [JsonProperty("displayTitle")]
        public HashName DisplayTitle { get; set; }

        [JsonProperty("displayDetail")]
        public HashName DisplayDetail { get; set; }

        [JsonProperty("briefPicPath")]
        public string BriefPicPath { get; set; }

        [JsonProperty("detailPicPath")]
        public string DetailPicPath { get; set; }

        [JsonProperty("luaFile")]
        public string LuaFile { get; set; }

        [JsonProperty("challengeList")]
        public ChallengeList[] ChallengeList { get; set; }

        [JsonProperty("IsActChallenge")]
        public bool IsActChallenge { get; set; }

        [JsonProperty("fastBonusTime")]
        public int FastBonusTime { get; set; }

        [JsonProperty("sonicBonusTime")]
        public int SonicBonusTime { get; set; }

        [JsonProperty("hardLevel")]
        public int HardLevel { get; set; }

        [JsonProperty("hardLevelGroup")]
        public int HardLevelGroup { get; set; }

        [JsonProperty("reviveTimes")]
        public int ReviveTimes { get; set; }

        [JsonProperty("reviveCostType")]
        public int ReviveCostType { get; set; }

        [JsonProperty("ReviveUseTypeList")]
        public int[] ReviveUseTypeList { get; set; }

        [JsonProperty("teamNum")]
        public int TeamNum { get; set; }

        [JsonProperty("maxNumList")]
        public int[] MaxNumList { get; set; }

        [JsonProperty("restrictList")]
        public int[] RestrictList { get; set; }

        [JsonProperty("loseDescList")]
        public HashName[] LoseDescList { get; set; }

        [JsonProperty("RecordLevelType")]
        public int RecordLevelType { get; set; }

        [JsonProperty("UseDynamicHardLv")]
        public int UseDynamicHardLv { get; set; }

        [JsonProperty("isTrunk")]
        public bool IsTrunk { get; set; }

        [JsonProperty("MonsterAttrShow")]
        public int[] MonsterAttrShow { get; set; }

        [JsonProperty("playerGetAllDrops")]
        public bool PlayerGetAllDrops { get; set; }

        [JsonProperty("HardCoeff")]
        public int HardCoeff { get; set; }

        [JsonProperty("enterTimesType")]
        public int EnterTimesType { get; set; }

        [JsonProperty("isEnterWithElf")]
        public int IsEnterWithElf { get; set; }

        [JsonProperty("PreMissionList")]
        public int[] PreMissionList { get; set; }

        [JsonProperty("LockedText")]
        public HashName LockedText { get; set; }

        [JsonProperty("PreMissionLink")]
        public int PreMissionLink { get; set; }

        [JsonProperty("PreMissionLinkParams")]
        public int[] PreMissionLinkParams { get; set; }

        [JsonProperty("PreMissionLinkParamStr")]
        public string PreMissionLinkParamStr { get; set; }

        [JsonProperty("UnlockedText")]
        public HashName UnlockedText { get; set; }

        [JsonProperty("UnlockedLink")]
        public int UnlockedLink { get; set; }

        [JsonProperty("UnlockedLinkParams")]
        public int[] UnlockedLinkParams { get; set; }

        [JsonProperty("UnlockedLinkParamStr")]
        public string UnlockedLinkParamStr { get; set; }

        [JsonProperty("costMaterialId")]
        public int CostMaterialId { get; set; }

        [JsonProperty("costMaterialNum")]
        public int CostMaterialNum { get; set; }

        [JsonProperty("firstCostMaterialNum")]
        public int FirstCostMaterialNum { get; set; }

        [JsonProperty("BalanceModeType")]
        public int BalanceModeType { get; set; }

        [JsonProperty("StageEntryNameList")]
        public string[] StageEntryNameList { get; set; }

        [JsonProperty("FloatDrop")]
        public FloatDrop[] FloatDrop { get; set; }

        [JsonProperty("IsBattleYLevel")]
        public bool IsBattleYLevel { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("levelId")]
        public int LevelId { get; set; }
    }

    public partial class ChallengeList
    {
        [JsonProperty("challengeId")]
        public int ChallengeId { get; set; }

        [JsonProperty("rewardId")]
        public int RewardId { get; set; }
    }

    public partial class FloatDrop
    {
        [JsonProperty("materialId")]
        public long MaterialId { get; set; }

        [JsonProperty("maxNum")]
        public long MaxNum { get; set; }

        [JsonProperty("minNum")]
        public long MinNum { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
