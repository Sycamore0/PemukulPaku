using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class FrameData : BaseExcelReader<FrameData, FrameDataExcel>
    {
        public override string FileName { get { return "FrameData.json"; } }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class FrameDataExcel
    {
        [JsonProperty("name")]
        public HashName Name { get; set; }

        [JsonProperty("rarity")]
        public int Rarity { get; set; }

        [JsonProperty("desc")]
        public HashName Desc { get; set; }

        [JsonProperty("iconPath")]
        public string IconPath { get; set; }

        [JsonProperty("imagePath")]
        public string ImagePath { get; set; }

        [JsonProperty("isShow")]
        public bool IsShow { get; set; }

        [JsonProperty("UIShowOrder")]
        public int UiShowOrder { get; set; }

        [JsonProperty("Type")]
        public int Type { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
