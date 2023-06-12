using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class OpenWorldCycleData : BaseExcelReader<OpenWorldCycleData, OpenWorldCycleDataExcel>
    {
        public override string FileName { get { return "OpenWorldCycleData.json"; } }

        public uint GetInitCycle(uint mapId)
        {
            return (uint?)All.Where(x => x.CycleMap == mapId).OrderBy(x => x.Cycle).FirstOrDefault()?.Cycle ?? 0;
        }
        
        public uint GetNextCycle(uint mapId, uint cycle)
        {
            return (uint?)All.Where(x => x.CycleMap == mapId && x.Cycle > cycle).OrderBy(x => x.Cycle).FirstOrDefault()?.Cycle ?? cycle;
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class OpenWorldCycleDataExcel
    {
        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("needstory")]
        public int Needstory { get; set; }

        [JsonProperty("finishreward")]
        public int Finishreward { get; set; }

        [JsonProperty("cycleMap")]
        public int CycleMap { get; set; }

        [JsonProperty("hardLvKey")]
        public string HardLvKey { get; set; }

        [JsonProperty("CycleName")]
        public HashName CycleName { get; set; }

        [JsonProperty("EntranceScene")]
        public int EntranceScene { get; set; }

        [JsonProperty("EntranceImg1")]
        public string EntranceImg1 { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("cycle")]
        public int Cycle { get; set; }
    }
}
