using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> FilterTeamsAsync(string filter = default(string), int pageNumber = 1, int pageSize = 10);

        Task RebaseTeamsAsync(string accessToken);
    }
}
