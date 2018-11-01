using ESportStatistics.Data.Models.Contracts;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESportStatistics.Data.Models.Abstract
{
    public abstract class BaseEntity : IEntity, IAuditable, IDeletable
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}
