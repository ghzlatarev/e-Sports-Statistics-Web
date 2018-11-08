using ESportStatistics.Data.Models.Identity;
using ESportStatistics.Web.Models;
using PagedList.Core;
using System.Linq;

namespace ESportStatistics.Web.Areas.Administration.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(IPagedList<ApplicationUser> users, string searchTerm = "")
        {
            this.Table = new TableViewModel<UserViewModel>()
            {
                Items = users.Select(u => new UserViewModel(u)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = users.PageCount,
                    PageNumber = users.PageNumber,
                    PageSize = users.PageSize,
                    HasNextPage = users.HasNextPage,
                    HasPreviousPage = users.HasPreviousPage,
                    SearchTerm = searchTerm,
                    AreaRoute = "Administration",
                    ControllerRoute = "User",
                    ActionRoute = "Index"
                }
            };
        }

        public TableViewModel<UserViewModel> Table { get; set; }
    }
}
