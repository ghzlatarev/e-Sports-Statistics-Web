using Newtonsoft.Json;
using ESportStatistics.Data.Models.Abstract;

namespace ESportStatistics.Data.Models
{
    public class Player : PandaScoreBaseEntity
    {
        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("hometown")]
        public string Hometown { get; set; }

        [JsonProperty("image_url")]
        public string ImageURL { get; set; }

        [JsonProperty("current_team/id")]
        public int? CurrentTeamId { get; set; }

        [JsonIgnore]
        public Team Team { get; set; }
    }
}
