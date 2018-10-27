using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ESportStatistics.Data.Models.Abstract
{
    public abstract class PandaScoreBaseEntity : BaseEntity
    {
        [Required]
        [JsonProperty("Id")]
        public int PandaScoreId { get; set; }
    }
}
