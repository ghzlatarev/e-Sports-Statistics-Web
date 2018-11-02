using ESportStatistics.Data.Models.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ESportStatistics.Data.Models.Identity
{
    public class ApplicationUser : IdentityUser, IDeletable, IAuditable
    {
        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
        
        public byte[] AvatarImage { get; set; }
    }
}
