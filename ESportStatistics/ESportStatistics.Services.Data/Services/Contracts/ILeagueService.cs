using ESportStatistics.Data.Models;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ILeagueService
    {
        Task<IPagedList<League>> FilterLeaguesAsync(string filter ="", int pageNumber = 1, int pageSize = 10);

        Task<League> FindAsync(string id);

        Task RebaseLeaguesAsync(string accessToken);
    }
}
