using ESportStatistics.Data.Models.Contracts;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ESportStatistics.Data.Models.Abstract
{
    public abstract class PandaScoreBaseEntity : BaseEntity, IPandaScoreBaseEntity
    {
        [Required]
        [JsonProperty("Id")]
        public int PandaScoreId { get; set; }
    }
}
