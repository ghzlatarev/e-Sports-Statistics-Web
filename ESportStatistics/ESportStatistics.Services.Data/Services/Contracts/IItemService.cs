using ESportStatistics.Data.Models;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IItemService
    {
        Task<IPagedList<Item>> FilterItemsAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10);

        Task<Item> FindAsync(string id);

        Task RebaseItemsAsync(string accessToken);
    }
}
