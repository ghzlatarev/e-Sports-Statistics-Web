using ESportStatistics.Data.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace ESportStatistics.Web.Areas.Identity.ManageViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(ApplicationUser user, string statusMessage)
        {
            this.Username = user.UserName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.IsEmailConfirmed = user.EmailConfirmed;
            this.StatusMessage = statusMessage;
            this.AvatarImage = user.AvatarImage;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public byte[] AvatarImage { get; set; }

        public string StatusMessage { get; set; }
    }
}
