using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using System.Linq;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Series
{
    public class SerieIndexViewModel
    {
        public SerieIndexViewModel(IPagedList<Serie> series, string sortOrder = "", string searchTerm = "")
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
                    SortOrder = sortOrder,
                    AreaRoute = "Statistics",
                    ControllerRoute = "Serie",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<SerieViewModel> Table { get; set; }
    }
}
