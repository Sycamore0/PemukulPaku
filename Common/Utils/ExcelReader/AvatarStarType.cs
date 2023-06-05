using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class AvatarStarType : BaseExcelReader<AvatarStarType, AvatarStarTypeExcel>
    {
        public override string FileName { get { return "AvatarStarType.json"; } }

        public StarInfo GetNextStar(int avatarType, int starUpType, StarInfo currentStarInfo)
        {
            AvatarStarTypeExcel? currStarTypeExcel = All.FirstOrDefault(x => x.AvatarType == avatarType && x.AvatarStarUpType == starUpType && x.Star == currentStarInfo.Star && x.SubStar == currentStarInfo.SubStar);
            if (currStarTypeExcel is not null)
                currentStarInfo.Cost = currStarTypeExcel.Upgrade;

            AvatarStarTypeExcel? starTypeExcel = All.FirstOrDefault(x => x.AvatarType == avatarType && x.AvatarStarUpType == starUpType && x.Star == ((currentStarInfo.SubStar == 3 || currentStarInfo.Star < 3) ? currentStarInfo.Star + 1 : currentStarInfo.Star) && x.SubStar == ((currentStarInfo.SubStar < 3 && currentStarInfo.Star >= 3) ? currentStarInfo.SubStar + 1 : 0));
            if (starTypeExcel is not null)
            {
                currentStarInfo.SubStar = starTypeExcel.SubStar;
                currentStarInfo.Star = starTypeExcel.Star;
            }
            return currentStarInfo;
        }

        public struct StarInfo
        {
            public StarInfo(int star, int subStar)
            {
                Star = star;
                SubStar = subStar;
            }

            public int Star { get; set; }
            public int SubStar { get; set; }
            public int Cost { get; set; } = 0;
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class AvatarStarTypeExcel
    {
        [JsonProperty("upgrade")]
        public int Upgrade { get; set; }

        [JsonProperty("IconPath")]
        public string IconPath { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("Star")]
        public int Star { get; set; }

        [JsonProperty("SubStar")]
        public int SubStar { get; set; }

        [JsonProperty("avatarType")]
        public int AvatarType { get; set; }

        [JsonProperty("avatarStarUpType")]
        public int AvatarStarUpType { get; set; }
    }
}
