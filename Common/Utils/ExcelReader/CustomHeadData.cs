using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class CustomHeadData : BaseExcelReader<CustomHeadData, CustomHeadDataExcel>
    {
        public override string FileName { get { return "CustomHeadData.json"; } }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class CustomHeadDataExcel
    {
        [JsonProperty("indexID")]
        public int IndexId { get; set; }

        [JsonProperty("Page")]
        public int Page { get; set; }

        [JsonProperty("headTitle")]
        public string HeadTitle { get; set; }

        [JsonProperty("headDescription")]
        public string HeadDescription { get; set; }

        [JsonProperty("iconPath")]
        public string IconPath { get; set; }

        [JsonProperty("imagePath")]
        public string ImagePath { get; set; }

        [JsonProperty("Show")]
        public bool Show { get; set; }

        [JsonProperty("HeadType")]
        public int HeadType { get; set; }

        [JsonProperty("HeadParaInt")]
        public int HeadParaInt { get; set; }

        [JsonProperty("TimeType")]
        public int TimeType { get; set; }

        [JsonProperty("During")]
        public int During { get; set; }

        [JsonProperty("EndTime")]
        public string EndTime { get; set; }

        [JsonProperty("BgColorPath")]
        public string BgColorPath { get; set; }

        [JsonProperty("rarity")]
        public int Rarity { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("headID")]
        public int HeadId { get; set; }
    }
}
