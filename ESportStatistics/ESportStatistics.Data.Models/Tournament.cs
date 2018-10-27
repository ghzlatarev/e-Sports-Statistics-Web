using Newtonsoft.Json;
using System.Collections.Generic;
using ESportStatistics.Data.Models.Abstract;

namespace ESportStatistics.Data.Models
{
    public class Tournament : PandaScoreBaseEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("begin_at")]
        public string BeginAt { get; set; }

        [JsonProperty("end_at")]
        public string EndAt { get; set; }

        [JsonProperty("serie_id")]
        public int? SerieId { get; set; }

        [JsonProperty("league_id")]
        public int? LeagueId { get; set; }

        [JsonIgnore]
        public Serie Serie { get; set; }

        [JsonIgnore]
        public League League { get; set; }

        [JsonIgnore]
        public ICollection<Match> Matches { get; set; }
    }
}
