using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IMasteryService
    {
        Task<IEnumerable<Mastery>> FilterMasteriesAsync(string filter = "", int pageNumber = 1, int pageSize = 10);

        Task RebaseMasteriesAsync(string accessToken);
    }
}
