using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class SingleWantedStageGroup : BaseExcelReader<SingleWantedStageGroup, SingleWantedStageGroupExcel>
    {
        public override string FileName { get { return "SingleWantedStageGroup.json"; } }

        public SingleWantedStageGroupExcel? FromGroupId(int groupId)
        {
            return All.FirstOrDefault(group => group.StageGroupId == groupId);
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class SingleWantedStageGroupExcel
    {
        [JsonProperty("StageIDList")]
        public int[] StageIdList { get; set; }

        [JsonProperty("MPStageIDList")]
        public int[] MpStageIdList { get; set; }

        [JsonProperty("StageGroupThemeID")]
        public int StageGroupThemeId { get; set; }

        [JsonProperty("StageGroupDesc")]
        public HashName StageGroupDesc { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("StageGroupID")]
        public int StageGroupId { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
