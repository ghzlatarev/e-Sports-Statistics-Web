using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Players
{
    public class IndexViewModel
    {
        public IndexViewModel(IPagedList<Player> players, string searchTerm = "")
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
                    AreaRoute = "Statistics",
                    ControllerRoute = "Player",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<PlayerViewModel> Table { get; set; }
    }
}
