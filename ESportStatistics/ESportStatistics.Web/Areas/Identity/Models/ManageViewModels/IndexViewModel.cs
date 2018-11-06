using ESportStatistics.Data.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace ESportStatistics.Web.Areas.Identity.ManageViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {

        }

        public IndexViewModel(ApplicationUser user, string statusMessage)
        {
            this.Username = user.UserName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.IsEmailConfirmed = user.EmailConfirmed;
            this.StatusMessage = statusMessage;
            this.AvatarImage = user.AvatarImage;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Address = user.Address;
            this.City = user.City;
            this.Country = user.Country;
            this.PostalCode = user.PostalCode;
            this.Story = user.Story;
        }

        [Required]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public byte[] AvatarImage { get; set; }

        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int PostalCode { get; set; }

        public string Story { get; set; }

        public string StatusMessage { get; set; }
    }
}
