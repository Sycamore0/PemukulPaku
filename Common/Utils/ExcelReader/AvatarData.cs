using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class AvatarData : BaseExcelReader<AvatarData, AvatarDataExcel>
    {
        public override string FileName { get { return "AvatarData.json"; } }

        public AvatarDataExcel? FromId(int id)
        {
            return All.Where(avatar => avatar.AvatarId == id).FirstOrDefault();
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class AvatarDataExcel
    {
        [JsonProperty("classID")]
        public int ClassId { get; set; }

        [JsonProperty("roleID")]
        public int RoleId { get; set; }

        [JsonProperty("avatarType")]
        public int AvatarType { get; set; }

        [JsonProperty("fullName")]
        public HashName FullName { get; set; }

        [JsonProperty("shortName")]
        public HashName ShortName { get; set; }

        [JsonProperty("RomaName")]
        public HashName RomaName { get; set; }

        [JsonProperty("desc")]
        public HashName Desc { get; set; }

        [JsonProperty("avatarRegistryKey")]
        public string AvatarRegistryKey { get; set; }

        [JsonProperty("weaponBaseTypeList")]
        public int[] WeaponBaseTypeList { get; set; }

        [JsonProperty("unlockStar")]
        public int UnlockStar { get; set; }

        [JsonProperty("skillList")]
        public int[] SkillList { get; set; }

        [JsonProperty("attribute")]
        public int Attribute { get; set; }

        [JsonProperty("initialWeapon")]
        public int InitialWeapon { get; set; }

        [JsonProperty("avatarCardID")]
        public int AvatarCardId { get; set; }

        [JsonProperty("avatarFragmentID")]
        public int AvatarFragmentId { get; set; }

        [JsonProperty("artifactFragmentID")]
        public int ArtifactFragmentId { get; set; }

        [JsonProperty("ultraSkillID")]
        public int UltraSkillId { get; set; }

        [JsonProperty("captainSkillID")]
        public int CaptainSkillId { get; set; }

        [JsonProperty("SKL01SP")]
        public double Skl01Sp { get; set; }

        [JsonProperty("SKL01SPNeed")]
        public double Skl01SpNeed { get; set; }

        [JsonProperty("SKL01Charges")]
        public double Skl01Charges { get; set; }

        [JsonProperty("SKL01CD")]
        public double Skl01Cd { get; set; }

        [JsonProperty("SKL02SP")]
        public double Skl02Sp { get; set; }

        [JsonProperty("SKL02SPNeed")]
        public double Skl02SpNeed { get; set; }

        [JsonProperty("SKL02Charges")]
        public double Skl02Charges { get; set; }

        [JsonProperty("SKL02CD")]
        public double Skl02Cd { get; set; }

        [JsonProperty("SKL03SP")]
        public double Skl03Sp { get; set; }

        [JsonProperty("SKL03SPNeed")]
        public double Skl03SpNeed { get; set; }

        [JsonProperty("SKL03Charges")]
        public double Skl03Charges { get; set; }

        [JsonProperty("SKL03CD")]
        public double Skl03Cd { get; set; }

        [JsonProperty("SKL02ArtifactCD")]
        public double Skl02ArtifactCd { get; set; }

        [JsonProperty("SKL02ArtifactSP")]
        public double Skl02ArtifactSp { get; set; }

        [JsonProperty("SKL02ArtifactSPNeed")]
        public double Skl02ArtifactSpNeed { get; set; }

        [JsonProperty("baseAvatarID")]
        public int BaseAvatarId { get; set; }

        [JsonProperty("firstName")]
        public HashName FirstName { get; set; }

        [JsonProperty("lastName")]
        public HashName LastName { get; set; }

        [JsonProperty("enFirstName")]
        public HashName EnFirstName { get; set; }

        [JsonProperty("enLastName")]
        public HashName EnLastName { get; set; }

        [JsonProperty("UISelectVoice")]
        public string UiSelectVoice { get; set; }

        [JsonProperty("UILevelUpVoice")]
        public string UiLevelUpVoice { get; set; }

        [JsonProperty("DA_Name")]
        public string DaName { get; set; }

        [JsonProperty("DA_Type")]
        public string DaType { get; set; }

        [JsonProperty("ArtifactID")]
        public int ArtifactId { get; set; }

        [JsonProperty("isEasterner")]
        public bool IsEasterner { get; set; }

        [JsonProperty("FaceAnimationGroupName")]
        public string FaceAnimationGroupName { get; set; }

        [JsonProperty("AvatarEffects")]
        public object[] AvatarEffects { get; set; }

        [JsonProperty("TagUnlockList")]
        public int[] TagUnlockList { get; set; }

        [JsonProperty("DefaultDressId")]
        public int DefaultDressId { get; set; }

        [JsonProperty("avatarStarUpType")]
        public int AvatarStarUpType { get; set; }

        [JsonProperty("avatarStarSourceID")]
        public object[] AvatarStarSourceId { get; set; }

        [JsonProperty("IsCollaboration")]
        public bool IsCollaboration { get; set; }

        [JsonProperty("StarUpBG")]
        public string StarUpBg { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("avatarID")]
        public int AvatarId { get; set; }
    }

    public partial class HashName
    {
        [JsonProperty("hash")]
        public long Hash { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
