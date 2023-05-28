using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class StigmataData : BaseExcelReader<StigmataData, StigmataDataExcel>
    {
        public override string FileName { get { return "StigmataData.json"; } }
        public StigmataDataExcel? FromId(int id)
        {
            return All.Where(stigmata => stigmata.Id == id).FirstOrDefault();
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class StigmataDataExcel
    {
        [JsonProperty("rarity")]
        public int Rarity { get; set; }

        [JsonProperty("maxRarity")]
        public int MaxRarity { get; set; }

        [JsonProperty("subRarity")]
        public int SubRarity { get; set; }

        [JsonProperty("subMaxRarity")]
        public int SubMaxRarity { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("powerType")]
        public int PowerType { get; set; }

        [JsonProperty("maxLv")]
        public int MaxLv { get; set; }

        [JsonProperty("expType")]
        public int ExpType { get; set; }

        [JsonProperty("sellPriceBase")]
        public int SellPriceBase { get; set; }

        [JsonProperty("sellPriceAdd")]
        public int SellPriceAdd { get; set; }

        [JsonProperty("gearExpProvideBase")]
        public int GearExpProvideBase { get; set; }

        [JsonProperty("gearExpPorvideAdd")]
        public int GearExpPorvideAdd { get; set; }

        [JsonProperty("baseType")]
        public int BaseType { get; set; }

        [JsonProperty("LabelPath")]
        public string LabelPath { get; set; }

        [JsonProperty("displayTitle")]
        public HashName DisplayTitle { get; set; }

        [JsonProperty("displayDescription")]
        public HashName DisplayDescription { get; set; }

        [JsonProperty("displayNumber")]
        public int DisplayNumber { get; set; }

        [JsonProperty("iconPath")]
        public string IconPath { get; set; }

        [JsonProperty("imagePath")]
        public string ImagePath { get; set; }

        [JsonProperty("HPBase")]
        public double HpBase { get; set; }

        [JsonProperty("HPAdd")]
        public double HpAdd { get; set; }

        [JsonProperty("SPBase")]
        public double SpBase { get; set; }

        [JsonProperty("SPAdd")]
        public double SpAdd { get; set; }

        [JsonProperty("attackBase")]
        public double AttackBase { get; set; }

        [JsonProperty("attackAdd")]
        public double AttackAdd { get; set; }

        [JsonProperty("defenceBase")]
        public double DefenceBase { get; set; }

        [JsonProperty("defenceAdd")]
        public double DefenceAdd { get; set; }

        [JsonProperty("criticalBase")]
        public double CriticalBase { get; set; }

        [JsonProperty("criticalAdd")]
        public double CriticalAdd { get; set; }

        [JsonProperty("durabilityMax")]
        public int DurabilityMax { get; set; }

        [JsonProperty("evoMaterial")]
        public Material[] EvoMaterial { get; set; }

        [JsonProperty("evoID")]
        public int EvoId { get; set; }

        [JsonProperty("prop1ID")]
        public double Prop1Id { get; set; }

        [JsonProperty("prop1Param1")]
        public double Prop1Param1 { get; set; }

        [JsonProperty("prop1Param2")]
        public double Prop1Param2 { get; set; }

        [JsonProperty("prop1Param3")]
        public double Prop1Param3 { get; set; }

        [JsonProperty("prop1Param1Add")]
        public double Prop1Param1Add { get; set; }

        [JsonProperty("prop1Param2Add")]
        public double Prop1Param2Add { get; set; }

        [JsonProperty("prop1Param3Add")]
        public double Prop1Param3Add { get; set; }

        [JsonProperty("prop2ID")]
        public double Prop2Id { get; set; }

        [JsonProperty("prop2Param1")]
        public double Prop2Param1 { get; set; }

        [JsonProperty("prop2Param2")]
        public double Prop2Param2 { get; set; }

        [JsonProperty("prop2Param3")]
        public double Prop2Param3 { get; set; }

        [JsonProperty("prop2Param1Add")]
        public double Prop2Param1Add { get; set; }

        [JsonProperty("prop2Param2Add")]
        public double Prop2Param2Add { get; set; }

        [JsonProperty("prop2Param3Add")]
        public double Prop2Param3Add { get; set; }

        [JsonProperty("prop3ID")]
        public double Prop3Id { get; set; }

        [JsonProperty("prop3Param1")]
        public double Prop3Param1 { get; set; }

        [JsonProperty("prop3Param2")]
        public double Prop3Param2 { get; set; }

        [JsonProperty("prop3Param3")]
        public double Prop3Param3 { get; set; }

        [JsonProperty("prop3Param1Add")]
        public double Prop3Param1Add { get; set; }

        [JsonProperty("prop3Param2Add")]
        public double Prop3Param2Add { get; set; }

        [JsonProperty("prop3Param3Add")]
        public double Prop3Param3Add { get; set; }

        [JsonProperty("protect")]
        public bool Protect { get; set; }

        [JsonProperty("setID")]
        public int SetId { get; set; }

        [JsonProperty("smallIcon")]
        public string SmallIcon { get; set; }

        [JsonProperty("tattooPath")]
        public string TattooPath { get; set; }

        [JsonProperty("offsetX")]
        public int OffsetX { get; set; }

        [JsonProperty("offsetY")]
        public int OffsetY { get; set; }

        [JsonProperty("scale")]
        public double Scale { get; set; }

        [JsonProperty("affixTreeId")]
        public int AffixTreeId { get; set; }

        [JsonProperty("canRefine")]
        public bool CanRefine { get; set; }

        [JsonProperty("recycleID")]
        public int RecycleId { get; set; }

        [JsonProperty("disjoinScoinCost")]
        public int DisjoinScoinCost { get; set; }

        [JsonProperty("disjoinAddMaterial")]
        public Material[] DisjoinAddMaterial { get; set; }

        [JsonProperty("LinkIDList")]
        public int[] LinkIdList { get; set; }

        [JsonProperty("quality")]
        public int Quality { get; set; }

        [JsonProperty("stigmataMainID")]
        public int StigmataMainId { get; set; }

        [JsonProperty("ShortName")]
        public HashName ShortName { get; set; }

        [JsonProperty("SellPriceID")]
        public int SellPriceId { get; set; }

        [JsonProperty("Transcendent")]
        public bool Transcendent { get; set; }

        [JsonProperty("Target")]
        public int Target { get; set; }

        [JsonProperty("isSecurityProtect")]
        public bool IsSecurityProtect { get; set; }

        [JsonProperty("GachaMainDropDisplayConfig")]
        public double[] GachaMainDropDisplayConfig { get; set; }

        [JsonProperty("GachaGiftDropDisplayConfig")]
        public double[] GachaGiftDropDisplayConfig { get; set; }

        [JsonProperty("StigmataFilterList")]
        public int[] StigmataFilterList { get; set; }

        [JsonProperty("CollaborationSetID")]
        public int CollaborationSetId { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("ID")]
        public int Id { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
