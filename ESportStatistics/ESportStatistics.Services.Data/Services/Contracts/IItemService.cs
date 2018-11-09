using ESportStatistics.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> FilterItemsAsync(string filter = "", int pageNumber = 1, int pageSize = 10);

        Task<Item> ReturnItemsAsync(Guid id);

        Task RebaseItemsAsync(string accessToken);
    }
}
