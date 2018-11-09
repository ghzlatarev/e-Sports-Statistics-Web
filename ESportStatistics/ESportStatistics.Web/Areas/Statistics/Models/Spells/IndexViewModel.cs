using ESportStatistics.Data.Models;
using ESportStatistics.Web.Models;
using System.Linq;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Models.Spells
{
    public class IndexViewModel
    {
        public IndexViewModel(IPagedList<Spell> spells, string searchTerm = "")
        {
            this.Table = new TableViewModel<SpellViewModel>()
            {
                Items = spells.Select(s => new SpellViewModel(s)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = spells.PageCount,
                    PageNumber = spells.PageNumber,
                    PageSize = spells.PageSize,
                    HasNextPage = spells.HasNextPage,
                    HasPreviousPage = spells.HasPreviousPage,
                    SearchTerm = searchTerm,
                    AreaRoute = "Statistics",
                    ControllerRoute = "Spell",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<SpellViewModel> Table { get; set; }
    }
}
