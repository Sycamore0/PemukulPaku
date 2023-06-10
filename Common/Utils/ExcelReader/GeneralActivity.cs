using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class GeneralActivity : BaseExcelReader<GeneralActivity, GeneralActivityExcel>
    {
        public override string FileName { get { return "GeneralActivity.json"; } }

        public List<GeneralActivityExcel> GetAllDistinct()
        {
            return All.Distinct(new GeneralActivityDisplayComparer()).ToList();
        }
    }

    public class GeneralActivityDisplayComparer : IEqualityComparer<GeneralActivityExcel>
    {
#pragma warning disable CS8767
        public bool Equals(GeneralActivityExcel x, GeneralActivityExcel y)
        {
            return x.DisplayData == y.DisplayData;
        }
#pragma warning restore CS8767

        public int GetHashCode(GeneralActivityExcel obj)
        {
            return obj.DisplayData;
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class GeneralActivityExcel
    {
        [JsonProperty("Series")]
        public int Series { get; set; }

        [JsonProperty("AcitivityType")]
        public int AcitivityType { get; set; }

        [JsonProperty("MinLevel")]
        public int MinLevel { get; set; }

        [JsonProperty("MaxLevel")]
        public int MaxLevel { get; set; }

        [JsonProperty("LinkData")]
        public int LinkData { get; set; }

        [JsonProperty("DisplayData")]
        public int DisplayData { get; set; }

        [JsonProperty("ScoreData")]
        public int ScoreData { get; set; }

        [JsonProperty("RankData")]
        public int RankData { get; set; }

        [JsonProperty("ShowRank")]
        public int ShowRank { get; set; }

        [JsonProperty("TeamListID")]
        public int TeamListId { get; set; }

        [JsonProperty("PreCondType")]
        public int PreCondType { get; set; }

        [JsonProperty("PreCondParaStr")]
        public string PreCondParaStr { get; set; }

        [JsonProperty("PreUnlockHint")]
        public HashName PreUnlockHint { get; set; }

        [JsonProperty("ActivityBuffID")]
        public int ActivityBuffId { get; set; }

        [JsonProperty("TicketIDList")]
        public int[] TicketIdList { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("AcitivityID")]
        public int AcitivityId { get; set; }
    }
}
