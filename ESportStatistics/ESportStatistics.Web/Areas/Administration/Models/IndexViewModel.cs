using ESportStatistics.Data.Models.Identity;
using System.Collections.Generic;
using System.Linq;

namespace ESportStatistics.Web.Areas.Administration.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(IEnumerable<ApplicationUser> users)
        {
            this.Users = users.Select(u => new UserViewModel(u)).ToList();
        }

        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
