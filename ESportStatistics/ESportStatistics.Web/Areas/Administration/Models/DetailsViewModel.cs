using ESportStatistics.Data.Models.Identity;

namespace ESportStatistics.Web.Areas.Administration.Models
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {


        }

        public DetailsViewModel(ApplicationUser user, bool isAdmin)
        {
            this.Username = user.UserName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.IsEmailConfirmed = user.EmailConfirmed;
            this.AvatarImage = user.AvatarImage;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Address = user.Address;
            this.City = user.City;
            this.Country = user.Country;
            this.PostalCode = user.PostalCode;
            this.Story = user.Story;
            this.UserRole = new UserRoleViewModel(user, isAdmin);
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public byte[] AvatarImage { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int PostalCode { get; set; }

        public string Story { get; set; }

        public UserRoleViewModel UserRole { get; set; }
    }
}
