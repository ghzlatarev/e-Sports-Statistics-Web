using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ITeamService
    {
        Task <IEnumerable<Team>> FilterTeamsAsync(string filter, int pageNumber, int pageSize);

        Task RebaseTeamsAsync(string accessToken);
    }
}
