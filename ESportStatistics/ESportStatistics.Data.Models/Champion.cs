using ESportStatistics.Data.Models.Abstract;
using Newtonsoft.Json;

namespace ESportStatistics.Data.Models
{
    public class Champion : PandaScoreBaseEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("armor")]
        public double? Armor { get; set; }

        [JsonProperty("armorperlevel")]
        public double? ArmorPerLevel { get; set; }

        [JsonProperty("attackdamage")]
        public double? AttackDamage { get; set; }

        [JsonProperty("attackdamageperlevel")]
        public double? AttackDamagePerLevel { get; set; }

        [JsonProperty("attackrange")]
        public double? AttackRange { get; set; }

        [JsonProperty("attackspeedoffset")]
        public double? AttackSpeedOffset { get; set; }

        [JsonProperty("attackspeedperlevel")]
        public double? AttackSpeedPerlevel { get; set; }

        [JsonProperty("crit")]
        public double? Crit { get; set; }

        [JsonProperty("critperlevel")]
        public double? CritPerLevel { get; set; }

        [JsonProperty("hp")]
        public double? HP { get; set; }

        [JsonProperty("hpperlevel")]
        public double? HPPerLevel { get; set; }

        [JsonProperty("hpregen")]
        public double? HPRegen { get; set; }

        [JsonProperty("hpregenperlevel")]
        public double? HPRegenPerLevel { get; set; }

        [JsonProperty("movespeed")]
        public double? Movespeed { get; set; }

        [JsonProperty("mp")]
        public double? MP { get; set; }

        [JsonProperty("mpperlevel")]
        public double? MPPerLevel { get; set; }

        [JsonProperty("mpregen")]
        public double? MPRegen { get; set; }

        [JsonProperty("mpregenperlevel")]
        public double? MPRegenPerLevel { get; set; }

        [JsonProperty("spellblock")]
        public double? SpellBlock { get; set; }

        [JsonProperty("spellblockperlevel")]
        public double? SpellBlockPerLevel { get; set; }

        [JsonProperty("image_url")]
        public string ImageURL { get; set; }

        [JsonProperty("big_image_url")]
        public string BigImageURL { get; set; }
    }
}
