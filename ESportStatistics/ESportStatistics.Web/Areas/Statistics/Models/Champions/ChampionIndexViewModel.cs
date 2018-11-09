using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using System.Linq;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Champions
{
    public class ChampionIndexViewModel
    {
        public ChampionIndexViewModel(IPagedList<Champion> champions, string searchTerm = "")
        {
            this.Table = new TableViewModel<ChampionViewModel>()
            {
                Items = champions.Select(c => new ChampionViewModel(c)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = champions.PageCount,
                    PageNumber = champions.PageNumber,
                    PageSize = champions.PageSize,
                    HasNextPage = champions.HasNextPage,
                    HasPreviousPage = champions.HasPreviousPage,
                    SearchTerm = searchTerm,
                    AreaRoute = "Statistics",
                    ControllerRoute = "Champion",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<ChampionViewModel> Table { get; set; }
    }
}
