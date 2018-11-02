using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IItemService
    {
        Task<IEnumerable<Champion>> FilterItemsAsync(string filter, int pageNumber = 1, int pageSize = 10);

        Task RebaseItemsAsync(string accessToken);
    }
}
