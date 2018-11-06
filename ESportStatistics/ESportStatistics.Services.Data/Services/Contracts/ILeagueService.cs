using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ILeagueService
    {
        Task<IEnumerable<League>> FilterLeaguesAsync(string filter ="", int pageNumber = 1, int pageSize = 10);

        Task RebaseLeaguesAsync(string accessToken);
    }
}
