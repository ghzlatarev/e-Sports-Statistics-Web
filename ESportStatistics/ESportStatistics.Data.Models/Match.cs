using Newtonsoft.Json;
using ESportStatistics.Data.Models.Abstract;

namespace ESportStatistics.Data.Models
{
    public class Match : PandaScoreBaseEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("begin_at")]
        public string BeginAt { get; set; }

        [JsonProperty("number_of_games")]
        public int? NumberOfGames { get; set; }

        [JsonProperty("draw")]
        public bool? Draw { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("match_type")]
        public string MatchType { get; set; }

        [JsonProperty("tournament_id")]
        public int? TournamentId { get; set; }

        [JsonIgnore]
        public Tournament Tournament { get; set; }
    }
}
