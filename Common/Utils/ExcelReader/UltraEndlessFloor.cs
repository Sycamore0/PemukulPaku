using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class UltraEndlessFloor : BaseExcelReader<UltraEndlessFloor, UltraEndlessFloorExcel>
    {
        public override string FileName { get { return "UltraEndlessFloor.json"; } }

        public List<UltraEndlessFloorExcel> GetFloorDatasFromStageId(int id)
        {
            return All.Where(floor => floor.StageId == id).ToList();
        }

        public UltraEndlessFloorExcel? FromStageAndFloorId(int stageId, int floorId)
        {
            return All.FirstOrDefault(x => x.FloorId == floorId && x.StageId == stageId);
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class UltraEndlessFloorExcel
    {
        [JsonProperty("NeedScore")]
        public int NeedScore { get; set; }

        [JsonProperty("MaxScore")]
        public int MaxScore { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("FloorID")]
        public int FloorId { get; set; }

        [JsonProperty("StageID")]
        public int StageId { get; set; }
    }
}
