using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    internal class AvatarSubSkillLevelData : BaseExcelReader<AvatarSubSkillLevelData, AvatarSubSkillLevelDataExcel>
    {
        public override string FileName { get { return "AvatarSubSkillLevelDataV2.json"; } }

        public List<AvatarSubSkillLevelDataExcel> FromType(int type)
        {
            return All.Where(d => d.ItemType == type).ToList();
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class AvatarSubSkillLevelDataExcel
    {
        [JsonProperty("ItemList_1")]
        public AvatarSubSkillLevelItemList[] ItemList1 { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("ItemType")]
        public int ItemType { get; set; }

        [JsonProperty("SkillLevel")]
        public int SkillLevel { get; set; }
    }

    public partial class AvatarSubSkillLevelItemList
    {
        [JsonProperty("itemMetaID")]
        public int ItemMetaId { get; set; }

        [JsonProperty("itemNum")]
        public int ItemNum { get; set; }
    }
}
