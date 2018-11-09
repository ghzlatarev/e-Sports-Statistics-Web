using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using System.Linq;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Players
{
    public class PlayerIndexViewModel
    {
        public PlayerIndexViewModel(IPagedList<Player> players, string sortOrder = "", string searchTerm = "")
        {
            this.Table = new TableViewModel<PlayerViewModel>()
            {
                Items = players.Select(p => new PlayerViewModel(p)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = players.PageCount,
                    PageNumber = players.PageNumber,
                    PageSize = players.PageSize,
                    HasNextPage = players.HasNextPage,
                    HasPreviousPage = players.HasPreviousPage,
                    SearchTerm = searchTerm,
                    SortOrder = sortOrder,
                    AreaRoute = "Statistics",
                    ControllerRoute = "Player",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<PlayerViewModel> Table { get; set; }
    }
}
