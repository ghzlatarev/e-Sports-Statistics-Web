﻿using ESportStatistics.Data.Models.Identity;

namespace ESportStatistics.Web.Areas.Administration.Models
{
    public class UserTableViewModel
    {
        public UserTableViewModel(ApplicationUser user)
        {
            this.Id = user.Id;
            this.Username = user.UserName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.IsDeleted = user.IsDeleted;
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsAdmin { get; set; }
    }
}
