using Newtonsoft.Json;
using ESportStatistics.Data.Models.Abstract;

namespace ESportStatistics.Data.Models
{
    public class Item : PandaScoreBaseEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gold_total")]
        public int? TotalGold { get; set; }

        [JsonProperty("gold_base")]
        public int? BaseGold { get; set; }

        [JsonProperty("gold_sell")]
        public int? SellGold { get; set; }

        [JsonProperty("gold_purchasable")]
        public bool? GoldPurchaseable { get; set; }

        [JsonProperty("flat_magic_damage_mod")]
        public int? FlatMagicDamageModifier { get; set; }

        [JsonProperty("flat_crit_chance_mod")]
        public int? FlatCritChanceModifier { get; set; }

        [JsonProperty("percent_attack_speed_mod")]
        public int? PercentAttackSpeedModifier { get; set; }

        [JsonProperty("flat_movement_speed_mod")]
        public int? PercentMovementSpeedModifier { get; set; }

        [JsonProperty("flat_armor_mod")]
        public int? FlatArmorModifier { get; set; }

        [JsonProperty("flat_spell_block_mod")]
        public int? FlatSpellBlockModifier { get; set; }

        [JsonProperty("flat_physical_damage_mod")]
        public int? FlatPhysicalDamageModifier { get; set; }

        [JsonProperty("percent_life_steal_mod")]
        public int? PercentLifeStealModifier { get; set; }

        [JsonProperty("flat_hp_regen_mod")]
        public int? FlatHeatlhRegenModifier { get; set; }

        [JsonProperty("flat_mp_regen_mod")]
        public int? FlatManaRegenModifier { get; set; }

        [JsonProperty("flat_mp_pool_mod")]
        public int? FlatManaPoolModifier { get; set; }

        [JsonProperty("image_url")]
        public string ImageURL { get; set; }
    }
}
