using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class EntryThemeItemData : BaseExcelReader<EntryThemeItemData, EntryThemeItemDataExcel>
    {
        public override string FileName { get { return "EntryThemeItemData.json"; } }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class EntryThemeItemDataExcel
    {
        [JsonProperty("UnlockType")]
        public int UnlockType { get; set; }

        [JsonProperty("UnlockPartID")]
        public int UnlockPartId { get; set; }

        [JsonProperty("Rarity")]
        public int Rarity { get; set; }

        [JsonProperty("ItemName")]
        public string ItemName { get; set; }

        [JsonProperty("ItemDesc")]
        public string ItemDesc { get; set; }

        [JsonProperty("iconPath")]
        public string IconPath { get; set; }

        [JsonProperty("ImagePath")]
        public string ImagePath { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("ThemeItemID")]
        public int ThemeItemId { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
