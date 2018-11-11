using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using System.Linq;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Tournaments
{
    public class TournamentIndexViewModel
    {
        public TournamentIndexViewModel(IPagedList<Tournament> tournaments, string sortOrder = "", string searchTerm = "")
        {
            this.Table = new TableViewModel<TournamentViewModel>()
            {
                Items = tournaments.Select(t => new TournamentViewModel(t)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = tournaments.PageCount,
                    PageNumber = tournaments.PageNumber,
                    PageSize = tournaments.PageSize,
                    HasNextPage = tournaments.HasNextPage,
                    HasPreviousPage = tournaments.HasPreviousPage,
                    SortOrder = sortOrder,
                    SearchTerm = searchTerm,
                    AreaRoute = "Statistics",
                    ControllerRoute = "Tournament",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<TournamentViewModel> Table { get; set; }
    }
}
