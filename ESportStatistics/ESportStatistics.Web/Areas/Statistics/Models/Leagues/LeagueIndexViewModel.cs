using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using System.Linq;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Leagues
{
    public class LeagueIndexViewModel
    {
        public LeagueIndexViewModel(IPagedList<League> leagues, string sortOrder = "", string searchTerm = "")
        {
            this.Table = new TableViewModel<LeagueViewModel>()
            {
                Items = leagues.Select(c => new LeagueViewModel(c)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = leagues.PageCount,
                    PageNumber = leagues.PageNumber,
                    PageSize = leagues.PageSize,
                    HasNextPage = leagues.HasNextPage,
                    HasPreviousPage = leagues.HasPreviousPage,
                    SortOrder = sortOrder,
                    SearchTerm = searchTerm,
                    AreaRoute = "Statistics",
                    ControllerRoute = "League",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<LeagueViewModel> Table { get; set; }
    }
}
