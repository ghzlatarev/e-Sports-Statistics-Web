using ESportStatistics.Data.Models;
using ESportStatistics.Web.Areas.Statistics.Models.Masteries;
using ESportStatistics.Web.Models;
using System.Linq;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Masteries
{
    public class MasteryIndexViewModel
    {
        public MasteryIndexViewModel(IPagedList<Mastery> masteries, string searchTerm = "")
        {
            this.Table = new TableViewModel<MasteryViewModel>()
            {
                Items = masteries.Select(c => new MasteryViewModel(c)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = masteries.PageCount,
                    PageNumber = masteries.PageNumber,
                    PageSize = masteries.PageSize,
                    HasNextPage = masteries.HasNextPage,
                    HasPreviousPage = masteries.HasPreviousPage,
                    SearchTerm = searchTerm,
                    AreaRoute = "Statistics",
                    ControllerRoute = "Mastery",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<MasteryViewModel> Table { get; set; }
    }
}
