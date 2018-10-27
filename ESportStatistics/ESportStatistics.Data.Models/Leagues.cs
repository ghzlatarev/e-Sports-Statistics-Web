using Newtonsoft.Json;
using System.Collections.Generic;
using ESportStatistics.Data.Models.Abstract;

namespace ESportStatistics.Data.Models
{
    public class League : PandaScoreBaseEntity
    {
        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("image_url")]
        public string ImageURL { get; set; }

        [JsonProperty("live_supported")]
        public bool? LifeSupported { get; set; }

        [JsonIgnore]
        public ICollection<Tournament> Tournaments { get; set; }

        [JsonIgnore]
        public ICollection<Serie> Series { get; set; }
    }
}
