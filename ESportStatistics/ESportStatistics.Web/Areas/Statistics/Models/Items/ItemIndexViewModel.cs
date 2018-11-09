using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using System.Linq;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Items
{
    public class ItemIndexViewModel
    {
        public ItemIndexViewModel(IPagedList<Item> items, string searchTerm = "")
        {
            this.Table = new TableViewModel<ItemViewModel>()
            {
                Items = items.Select(c => new ItemViewModel(c)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = items.PageCount,
                    PageNumber = items.PageNumber,
                    PageSize = items.PageSize,
                    HasNextPage = items.HasNextPage,
                    HasPreviousPage = items.HasPreviousPage,
                    SearchTerm = searchTerm,
                    AreaRoute = "Statistics",
                    ControllerRoute = "Item",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<ItemViewModel> Table { get; set; }
    }
}
