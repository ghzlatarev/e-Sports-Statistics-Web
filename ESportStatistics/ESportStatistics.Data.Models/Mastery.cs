using Newtonsoft.Json;
using ESportStatistics.Data.Models.Abstract;

namespace ESportStatistics.Data.Models
{
    public class Mastery : PandaScoreBaseEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
