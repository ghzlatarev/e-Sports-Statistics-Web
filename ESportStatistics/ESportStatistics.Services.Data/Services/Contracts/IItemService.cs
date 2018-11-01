using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IItemService
    {
        IEnumerable<Item> FilterItems(string filter, int pageNumber, int pageSize);

        Task RebaseItems(string accessToken);
    }
}
