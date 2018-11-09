using ESportStatistics.Data.Models;
using System;

namespace ESportStatistics.Web.Areas.Statistics.Models
{
    public class ItemDetailsViewModel
    {

        public ItemDetailsViewModel()
        {
        }

        public ItemDetailsViewModel(Item item)
        {

            //this.Name = item.Name;
            //this.BigImageURL = item.BigImageURL;
            //this.Armor = item.Armor;
            //this.ArmorPerLevel = item.ArmorPerLevel;
            //this.AttackDamage = item.AttackDamage;
            //this.AttackDamagePerLevel = item.AttackDamagePerLevel;
            //this.AttackRange = item.AttackRange;
            //this.AttackSpeedOffset = item.AttackSpeedOffset;
            //this.AttackSpeedPerlevel = item.AttackSpeedPerlevel;
            //this.Crit = item.Crit;
            //this.CritPerLevel = item.CritPerLevel;
            //this.HP = item.HP;
            //this.HPPerLevel = item.HPPerLevel;
            //this.HPRegen = item.HPRegen;
            //this.HPRegenPerLevel = item.HPRegenPerLevel;
            //this.Movespeed = item.Movespeed;
            //this.MP = item.MP;
            //this.MPPerLevel = item.MPPerLevel;
            //this.MPRegen = item.MPRegen;
            //this.MPRegenPerLevel = item.MPRegenPerLevel;
            //this.SpellBlock = item.SpellBlock;
            //this.SpellBlockPerLevel = item.SpellBlockPerLevel;
        }

        public string BigImageURL { get; set; }
        public string Name { get; set; }
        public double? Armor { get; set; }

        public double? ArmorPerLevel { get; set; }
        public double? AttackDamage { get; set; }
        public double? AttackDamagePerLevel { get; set; }
        public double? AttackRange { get; set; }
        public double? AttackSpeedOffset { get; set; }
        public double? AttackSpeedPerlevel { get; set; }
        public double? Crit { get; set; }
        public double? CritPerLevel { get; set; }
        public double? HP { get; set; }
        public double? HPPerLevel { get; set; }
        public double? HPRegen { get; set; }
        public double? HPRegenPerLevel { get; set; }
        public double? Movespeed { get; set; }
        public double? MP { get; set; }
        public double? MPPerLevel { get; set; }
        public double? MPRegen { get; set; }
        public double? MPRegenPerLevel { get; set; }
        public double? SpellBlock { get; set; }
        public double? SpellBlockPerLevel { get; set; }

    }
}
