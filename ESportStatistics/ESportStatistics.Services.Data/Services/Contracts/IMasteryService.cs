using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IMasteryService
    {
        Task<IPagedList<Mastery>> FilterMasteriesAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10);

        Task RebaseMasteriesAsync(string accessToken);
    }
}
