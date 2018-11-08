using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using System.Linq;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Tournaments
{
    public class IndexViewModel
    {
        public IndexViewModel(IPagedList<Tournament> tournaments, string searchTerm = "")
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
