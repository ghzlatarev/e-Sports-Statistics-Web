using ESportStatistics.Data.Models.Identity;

namespace ESportStatistics.Web.Areas.Administration.Models
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel(ApplicationUser user, bool isAdmin)
        {
            this.Id = user.Id;
            this.IsAdmin = isAdmin;
        }

        public string Id { get; set; }

        public bool IsAdmin { get; set; }
    }
}
