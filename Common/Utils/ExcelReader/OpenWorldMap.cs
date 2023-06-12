using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class OpenWorldMap : BaseExcelReader<OpenWorldMap, OpenWorldMapExcel>
    {
        public override string FileName { get { return "OpenWorldMap.json"; } }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class OpenWorldMapExcel
    {
        [JsonProperty("MapType")]
        public int MapType { get; set; }

        [JsonProperty("UnlockLv")]
        public int UnlockLv { get; set; }

        [JsonProperty("UnlockStoryId")]
        public int UnlockStoryId { get; set; }

        [JsonProperty("ShowTime")]
        public string ShowTime { get; set; }

        [JsonProperty("UnlockTime")]
        public string UnlockTime { get; set; }

        [JsonProperty("QuestSlotNum")]
        public int QuestSlotNum { get; set; }

        [JsonProperty("QuestSettleType")]
        public int QuestSettleType { get; set; }

        [JsonProperty("MapNameText")]
        public HashName MapNameText { get; set; }

        [JsonProperty("MapContentText")]
        public HashName MapContentText { get; set; }

        [JsonProperty("HpRecoverInterval")]
        public int HpRecoverInterval { get; set; }

        [JsonProperty("HpRecoverPercent")]
        public int HpRecoverPercent { get; set; }

        [JsonProperty("QuestMapUIManager")]
        public string QuestMapUiManager { get; set; }

        [JsonProperty("SelectDailyQuestPage")]
        public string SelectDailyQuestPage { get; set; }

        [JsonProperty("SettlementPage")]
        public string SettlementPage { get; set; }

        [JsonProperty("ShopOpenWorldPage")]
        public string ShopOpenWorldPage { get; set; }

        [JsonProperty("ShopTypeList")]
        public object[] ShopTypeList { get; set; }

        [JsonProperty("MapInfoText")]
        public HashName MapInfoText { get; set; }

        [JsonProperty("QuestInfoText")]
        public HashName QuestInfoText { get; set; }

        [JsonProperty("MapSelectPath")]
        public string MapSelectPath { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("MapId")]
        public int MapId { get; set; }
    }

}
