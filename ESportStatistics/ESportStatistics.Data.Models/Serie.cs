using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using ESportStatistics.Data.Models.Abstract;

namespace ESportStatistics.Data.Models
{
    public class Serie : PandaScoreBaseEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("begin_at")]
        public DateTime? BeginAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("end_at")]
        public DateTime? EndAt { get; set; }

        [JsonProperty("modified_at")]
        public DateTime? ModifiedAt { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("league_id")]
        public int? LeagueId { get; set; }

        [JsonIgnore]
        public League League { get; set; }

        [JsonIgnore]
        public ICollection<Tournament> Tournaments { get; set; }
    }
}
