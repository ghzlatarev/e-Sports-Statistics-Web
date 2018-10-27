using Newtonsoft.Json;
using System.Collections.Generic;
using ESportStatistics.Data.Models.Abstract;

namespace ESportStatistics.Data.Models
{
    public class Team : PandaScoreBaseEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("acronym")]
        public string Acronym { get; set; }

        [JsonProperty("image_url")]
        public string ImageURL { get; set; }

        [JsonIgnore]
        public ICollection<Player> Players { get; set; }
    }
}
