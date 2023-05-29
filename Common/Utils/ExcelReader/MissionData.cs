using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class MissionData : BaseExcelReader<MissionData, MissionDataExcel>
    {
        public override string FileName { get { return "MissionData.json"; } }

        public MissionDataExcel? FromId(int id)
        {
            return All.Where(mission => mission.Id == id).FirstOrDefault();
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class MissionDataExcel
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("subType")]
        public int SubType { get; set; }

        [JsonProperty("title")]
        public HashName Title { get; set; }

        [JsonProperty("description")]
        public HashName Description { get; set; }

        [JsonProperty("thumb")]
        public string Thumb { get; set; }

        [JsonProperty("finishWay")]
        public int FinishWay { get; set; }

        [JsonProperty("finishParaInt")]
        public int FinishParaInt { get; set; }

        [JsonProperty("finishParaStr")]
        public string FinishParaStr { get; set; }

        [JsonProperty("totalProgress")]
        public int TotalProgress { get; set; }

        [JsonProperty("rewardId")]
        public int RewardId { get; set; }

        [JsonProperty("LinkType")]
        public int LinkType { get; set; }

        [JsonProperty("LinkParams")]
        public int[] LinkParams { get; set; }

        [JsonProperty("TextmapTag")]
        public HashName TextmapTag { get; set; }

        [JsonProperty("LinkParamStr")]
        public string LinkParamStr { get; set; }

        [JsonProperty("PreviewTime")]
        public int PreviewTime { get; set; }

        [JsonProperty("NoDisplay")]
        public bool NoDisplay { get; set; }

        [JsonProperty("IsNeedDisplay")]
        public bool IsNeedDisplay { get; set; }

        [JsonProperty("Priority")]
        public int Priority { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
