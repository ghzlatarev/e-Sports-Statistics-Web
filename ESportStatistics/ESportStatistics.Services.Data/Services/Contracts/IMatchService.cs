using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IMatchService
    {
        Task<IPagedList<Match>> FilterMatchesAsync(string filter = "", int pageNumber = 1, int pageSize = 10);

        Task<Match> FindAsync(string id);

        Task RebaseMatchesAsync(string accessToken);
    }
}
