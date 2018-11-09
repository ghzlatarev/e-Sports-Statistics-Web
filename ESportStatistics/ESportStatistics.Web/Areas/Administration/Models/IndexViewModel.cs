using ESportStatistics.Data.Models.Identity;
using ESportStatistics.Web.Models;
using System.Linq;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Administration.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(IPagedList<ApplicationUser> users, string searchTerm = "")
        {
            this.Table = new TableViewModel<UserTableViewModel>()
            {
                Items = users.Select(u => new UserTableViewModel(u)),
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
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<UserTableViewModel> Table { get; set; }
    }
}
