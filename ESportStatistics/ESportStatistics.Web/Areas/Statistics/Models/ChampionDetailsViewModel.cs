using ESportStatistics.Data.Models;
using System;

namespace ESportStatistics.Web.Areas.Statistics.Models
{
    public class ChampionDetailsViewModel
    {

        public ChampionDetailsViewModel()
        {
        }

        public ChampionDetailsViewModel(Champion champion)
        {

            this.Name = champion.Name;
            this.BigImageURL = champion.BigImageURL;
            this.Armor = champion.Armor;
            this.ArmorPerLevel = champion.ArmorPerLevel;
            this.AttackDamage = champion.AttackDamage;
            this.AttackDamagePerLevel = champion.AttackDamagePerLevel;
            this.AttackRange = champion.AttackRange;
            this.AttackSpeedOffset = champion.AttackSpeedOffset;
            this.AttackSpeedPerlevel = champion.AttackSpeedPerlevel;
            this.Crit = champion.Crit;
            this.CritPerLevel = champion.CritPerLevel;
            this.HP = champion.HP;
            this.HPPerLevel = champion.HPPerLevel;
            this.HPRegen = champion.HPRegen;
            this.HPRegenPerLevel = champion.HPRegenPerLevel;
            this.Movespeed = champion.Movespeed;
            this.MP = champion.MP;
            this.MPPerLevel = champion.MPPerLevel;
            this.MPRegen = champion.MPRegen;
            this.MPRegenPerLevel = champion.MPRegenPerLevel;
            this.SpellBlock = champion.SpellBlock;
            this.SpellBlockPerLevel = champion.SpellBlockPerLevel;
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
