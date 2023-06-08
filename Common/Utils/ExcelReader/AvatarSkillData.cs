using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class AvatarSkillData : BaseExcelReader<AvatarSkillData, AvatarSkillDataExcel>
    {
        public override string FileName { get { return "AvatarSkillData.json"; } }

        public AvatarSkillDataExcel? FromId(int id)
        {
            return All.Where(subSkill => subSkill.SkillId == id).FirstOrDefault();
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class AvatarSkillDataExcel
    {
        [JsonProperty("name")]
        public HashName Name { get; set; }
        
        [JsonProperty("info")]
        public HashName Info { get; set; }
        
        [JsonProperty("showOrder")]
        public int ShowOrder { get; set; }

        [JsonProperty("unlockLv")]
        public int UnlockLv { get; set; }
        
        [JsonProperty("unlockStar")]
        public int UnlockStar { get; set; }
        
        [JsonProperty("skillStep")]
        public HashName SkillStep { get; set; }
        
        [JsonProperty("iconPath")]
        public string IconPath { get; set; }
        
        [JsonProperty("iconPathInLevel")]
        public string IconPathInLevel { get; set; }
        
        [JsonProperty("buttonName")]
        public string ButtonName { get; set; }

        [JsonProperty("paramBase_1")]
        public int ParamBase1 { get; set; }
        
        [JsonProperty("paramLogic_1")]
        public object[] ParamLogic1 { get; set; }
        
        [JsonProperty("paramSubID_1")]
        public int ParamSubID1 { get; set; }
        
        [JsonProperty("paramSubIndex_1")]
        public int ParamSubIndex1 { get; set; }
        
        [JsonProperty("paramBase_2")]
        public int ParamBase2 { get; set; }

        [JsonProperty("paramLogic_2")]
        public object[] ParamLogic2 { get; set; }

        [JsonProperty("paramSubID_2")]
        public int ParamSubID2 { get; set; }

        [JsonProperty("paramSubIndex_2")]
        public int ParamSubIndex2 { get; set; }
        
        [JsonProperty("paramBase_3")]
        public int ParamBase3 { get; set; }

        [JsonProperty("paramLogic_3")]
        public object[] ParamLogic3 { get; set; }

        [JsonProperty("paramSubID_3")]
        public int ParamSubID3 { get; set; }

        [JsonProperty("paramSubIndex_3")]
        public int ParamSubIndex3 { get; set; }

        [JsonProperty("canTry")]
        public bool CanTry { get; set; }
        
        [JsonProperty("TagList")]
        public TagList[] TagList { get; set; }

        [JsonProperty("unlockItemList")]
        public object UnlockItemList { get; set; }
        
        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }
        
        [JsonProperty("skillId")]
        public int SkillId { get; set; }
    }
}
