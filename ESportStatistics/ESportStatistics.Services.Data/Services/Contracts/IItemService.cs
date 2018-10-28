using ESportStatistics.Data.Models;
using System.Collections.Generic;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IItemService
    {
        IEnumerable<Item> FilterItems(string filter, int pageNumber, int pageSize);

        void RebaseItems();
    }
}
