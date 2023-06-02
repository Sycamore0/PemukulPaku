using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
    public class MaterialData : BaseExcelReader<MaterialData, MaterialDataExcel>
    {
        public override string FileName { get { return "MaterialData.json"; } }

        public MaterialDataExcel? FromId(uint id)
        {
            return All.Where(material => material.Id == id).FirstOrDefault();
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public partial class MaterialDataExcel
    {
        [JsonProperty("rarity")]
        public int Rarity { get; set; }

        [JsonProperty("maxRarity")]
        public int MaxRarity { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("maxLv")]
        public int MaxLv { get; set; }

        [JsonProperty("sellPriceBase_1")]
        public int SellPriceBase1 { get; set; }

        [JsonProperty("sellPriceAdd")]
        public int SellPriceAdd { get; set; }

        [JsonProperty("ServantExpProvide")]
        public int ServantExpProvide { get; set; }

        [JsonProperty("gearExpProvideBase")]
        public int GearExpProvideBase { get; set; }

        [JsonProperty("gearExpPorvideAdd")]
        public int GearExpPorvideAdd { get; set; }

        [JsonProperty("ItemType")]
        public string ItemType { get; set; }

        [JsonProperty("useID")]
        public int UseId { get; set; }

        [JsonProperty("BaseType")]
        public int BaseType { get; set; }

        [JsonProperty("displayTitle")]
        public HashName DisplayTitle { get; set; }

        [JsonProperty("displayDescription")]
        public HashName DisplayDescription { get; set; }

        [JsonProperty("iconPath")]
        public string IconPath { get; set; }

        [JsonProperty("imagePath")]
        public string ImagePath { get; set; }

        [JsonProperty("characterExpProvide")]
        public int CharacterExpProvide { get; set; }

        [JsonProperty("LinkIDList")]
        public int[] LinkIdList { get; set; }

        [JsonProperty("ShopUseList")]
        public string[] ShopUseList { get; set; }

        [JsonProperty("displayBGDescription")]
        public HashName DisplayBgDescription { get; set; }

        [JsonProperty("quantityLimit")]
        public int QuantityLimit { get; set; }

        [JsonProperty("SortID")]
        public int SortId { get; set; }

        [JsonProperty("AffixTrainType")]
        public int AffixTrainType { get; set; }

        [JsonProperty("AffixRandomValueIncress")]
        public int AffixRandomValueIncress { get; set; }

        [JsonProperty("AffixTitleExp")]
        public int AffixTitleExp { get; set; }

        [JsonProperty("quickBuyType")]
        public int QuickBuyType { get; set; }

        [JsonProperty("shopType")]
        public int ShopType { get; set; }

        [JsonProperty("idShopGoods")]
        public int IdShopGoods { get; set; }

        [JsonProperty("quickBuyConfirm")]
        public bool QuickBuyConfirm { get; set; }

        [JsonProperty("hideInInventory")]
        public bool HideInInventory { get; set; }

        [JsonProperty("hideNumInTips")]
        public bool HideNumInTips { get; set; }

        [JsonProperty("SellPriceID_1")]
        public int SellPriceId1 { get; set; }

        [JsonProperty("costVitality")]
        public int CostVitality { get; set; }

        [JsonProperty("enableQuickSell")]
        public bool EnableQuickSell { get; set; }

        [JsonProperty("advSellBonusNum")]
        public int AdvSellBonusNum { get; set; }

        [JsonProperty("TagType")]
        public int TagType { get; set; }

        [JsonProperty("GachaMainDropDisplayConfig")]
        public object[] GachaMainDropDisplayConfig { get; set; }

        [JsonProperty("GachaGiftDropDisplayConfig")]
        public object[] GachaGiftDropDisplayConfig { get; set; }

        [JsonProperty("alwaysShowPopUp")]
        public bool AlwaysShowPopUp { get; set; }

        [JsonProperty("DataImpl")]
        public object DataImpl { get; set; }

        [JsonProperty("ID")]
        public int Id { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}