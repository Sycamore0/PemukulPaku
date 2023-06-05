using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class MaterialUseData : BaseExcelReader<MaterialUseData, MaterialUseDataExcel>
    {
        public override string FileName { get { return "MaterialUseData.json"; } }

        public MaterialUseDataExcel? FromId(int id)
        {
            return All.Where(x => x.UseId == id).FirstOrDefault();
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class MaterialUseDataExcel
    {
        [JsonProperty("useType")]
        public int UseType { get; set; }

        [JsonProperty("MultiUse")]
        public int MultiUse { get; set; }

        [JsonProperty("MaterialUseConfirmType")]
        public int MaterialUseConfirmType { get; set; }

        [JsonProperty("paraStr")]
        public string[] ParaStr { get; set; }

        [JsonProperty("useTip")]
        public HashName UseTip { get; set; }

        [JsonProperty("useMin")]
        public int UseMin { get; set; }

        [JsonProperty("useMax")]
        public int UseMax { get; set; }

        [JsonProperty("AdditionDesc")]
        public string AdditionDesc { get; set; }

        [JsonProperty("EventID")]
        public int EventId { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("useID")]
        public int UseId { get; set; }
    }
}
