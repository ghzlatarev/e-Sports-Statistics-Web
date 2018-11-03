using ESportStatistics.Data.Models.Identity;

namespace ESportStatistics.Web.Areas.Administration.Models
{
    public class UserViewModel
    {
        public UserViewModel(ApplicationUser user)
        {
            this.Username = user.UserName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.IsDeleted = user.IsDeleted;
            this.AvatarImage = user.AvatarImage;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] AvatarImage { get; set; }
    }
}
