using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class DressData : BaseExcelReader<DressData, DressDataExcel>
    {
        public override string FileName { get { return "DressData.json"; } }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class DressDataExcel
    {
        [JsonProperty("name")]
        public HashName Name { get; set; }

        [JsonProperty("rarity")]
        public int Rarity { get; set; }

        [JsonProperty("avatarIDList")]
        public int[] AvatarIdList { get; set; }

        [JsonProperty("roleID")]
        public int RoleId { get; set; }

        [JsonProperty("iconPath")]
        public string IconPath { get; set; }

        [JsonProperty("dressType")]
        public int DressType { get; set; }

        [JsonProperty("getWay")]
        public HashName GetWay { get; set; }

        [JsonProperty("desc")]
        public HashName Desc { get; set; }

        [JsonProperty("coin")]
        public int Coin { get; set; }

        [JsonProperty("dressResource")]
        public string DressResource { get; set; }

        [JsonProperty("cardPath")]
        public string CardPath { get; set; }

        [JsonProperty("tachiePath")]
        public string TachiePath { get; set; }

        [JsonProperty("avatarIconPath")]
        public string AvatarIconPath { get; set; }

        [JsonProperty("avatarIconSidePath")]
        public string AvatarIconSidePath { get; set; }

        [JsonProperty("imagePath")]
        public string ImagePath { get; set; }

        [JsonProperty("LinkIDList")]
        public int[] LinkIdList { get; set; }

        [JsonProperty("avatarGachaFigure")]
        public string AvatarGachaFigure { get; set; }

        [JsonProperty("show")]
        public int Show { get; set; }

        [JsonProperty("PreviewStageID")]
        public int PreviewStageId { get; set; }

        [JsonProperty("DressTagList")]
        public int[] DressTagList { get; set; }

        [JsonProperty("RechargeShowName")]
        public HashName RechargeShowName { get; set; }

        [JsonProperty("RechargeShowAvatarName")]
        public HashName RechargeShowAvatarName { get; set; }

        [JsonProperty("RechargeShowPicPath")]
        public string RechargeShowPicPath { get; set; }

        [JsonProperty("ArtifactID")]
        public int ArtifactId { get; set; }

        [JsonProperty("MVPVoicePattern")]
        public string MvpVoicePattern { get; set; }

        [JsonProperty("UISelectVoice")]
        public string UiSelectVoice { get; set; }

        [JsonProperty("UILevelupVoice")]
        public string UiLevelupVoice { get; set; }

        [JsonProperty("GachaMainDropDisplayConfig")]
        public int[] GachaMainDropDisplayConfig { get; set; }

        [JsonProperty("GachaGiftDropDisplayConfig")]
        public object[] GachaGiftDropDisplayConfig { get; set; }

        [JsonProperty("IsCollaboration")]
        public bool IsCollaboration { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("dressID")]
        public int DressId { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
