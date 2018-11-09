using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Items
{
    public class ItemDetailsViewModel
    {

        public ItemDetailsViewModel()
        {
        }

        public ItemDetailsViewModel(Item item)
        {
            this.Name = item.Name;
            this.TotalGold = item.TotalGold;
            this.BaseGold = item.BaseGold;
            this.SellGold = item.SellGold;
            this.FlatMagicDamageModifier = item.FlatMagicDamageModifier;
            this.FlatCritChanceModifier = item.FlatCritChanceModifier;
            this.PercentAttackSpeedModifier = item.PercentAttackSpeedModifier;
            this.PercentMovementSpeedModifier = item.PercentMovementSpeedModifier;
            this.FlatArmorModifier = item.FlatArmorModifier;
            this.FlatSpellBlockModifier = item.FlatSpellBlockModifier;
            this.FlatPhysicalDamageModifier = item.FlatPhysicalDamageModifier;
            this.PercentLifeStealModifier = item.PercentLifeStealModifier;
            this.FlatHeatlhRegenModifier = item.FlatHeatlhRegenModifier;
            this.FlatManaRegenModifier = item.FlatManaRegenModifier;
            this.ImageURL = item.ImageURL;
        }

        public string Name { get; set; }
        public int? TotalGold { get; set; }
        public int? BaseGold { get; set; }
        public int? SellGold { get; set; }
        public int? FlatMagicDamageModifier { get; set; }
        public int? FlatCritChanceModifier { get; set; }
        public int? PercentAttackSpeedModifier { get; set; }
        public int? PercentMovementSpeedModifier { get; set; }
        public int? FlatArmorModifier { get; set; }
        public int? FlatSpellBlockModifier { get; set; }
        public int? FlatPhysicalDamageModifier { get; set; }
        public int? PercentLifeStealModifier { get; set; }
        public int? FlatHeatlhRegenModifier { get; set; }
        public int? FlatManaRegenModifier { get; set; }
        public int? FlatManaPoolModifier { get; set; }
        public string ImageURL { get; set; }

    }
}
