using Newtonsoft.Json;
using ESportStatistics.Data.Models.Abstract;

namespace ESportStatistics.Data.Models
{
    public class Spell : PandaScoreBaseEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image_url")]
        public string ImageURL { get; set; }
    }
}
