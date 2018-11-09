using System.Linq;
using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Teams
{
    public class IndexViewModel
    {
        public IndexViewModel(IPagedList<Team> teams, string searchTerm = "")
        {
            this.Table = new TableViewModel<TeamViewModel>()
            {
                Items = teams.Select(t => new TeamViewModel(t)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = teams.PageCount,
                    PageNumber = teams.PageNumber,
                    PageSize = teams.PageSize,
                    HasNextPage = teams.HasNextPage,
                    HasPreviousPage = teams.HasPreviousPage,
                    SearchTerm = searchTerm,
                    AreaRoute = "Statistics",
                    ControllerRoute = "Team",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<TeamViewModel> Table { get; set; }
    }
}
