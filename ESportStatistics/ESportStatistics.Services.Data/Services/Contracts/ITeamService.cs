using ESportStatistics.Data.Models;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ITeamService
    {
        Task<IPagedList<Team>> FilterTeamsAsync(string filter = "", int pageNumber = 1, int pageSize = 10);

        Task RebaseTeamsAsync(string accessToken);

        Task<Team> FindAsync(string id);
    }
}
