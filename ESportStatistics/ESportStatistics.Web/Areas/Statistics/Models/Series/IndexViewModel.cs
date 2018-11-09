using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Series
{
    public class IndexViewModel
    {
        public IndexViewModel(IPagedList<Serie> series, string searchTerm = "")
        {
            this.Table = new TableViewModel<SerieViewModel>()
            {
                Items = series.Select(s => new SerieViewModel(s)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = series.PageCount,
                    PageNumber = series.PageNumber,
                    PageSize = series.PageSize,
                    HasNextPage = series.HasNextPage,
                    HasPreviousPage = series.HasPreviousPage,
                    SearchTerm = searchTerm,
                    AreaRoute = "Statistics",
                    ControllerRoute = "Serie",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<SerieViewModel> Table { get; set; }
    }
}
