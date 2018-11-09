using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using System.Linq;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Matches
{
    public class MatchIndexViewModel
    {
        public MatchIndexViewModel(IPagedList<Match> matches, string searchTerm = "")
        {
            this.Table = new TableViewModel<MatchViewModel>()
            {
                Items = matches.Select(c => new MatchViewModel(c)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = matches.PageCount,
                    PageNumber = matches.PageNumber,
                    PageSize = matches.PageSize,
                    HasNextPage = matches.HasNextPage,
                    HasPreviousPage = matches.HasPreviousPage,
                    SearchTerm = searchTerm,
                    AreaRoute = "Statistics",
                    ControllerRoute = "Match",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<MatchViewModel> Table { get; set; }
    }
}
