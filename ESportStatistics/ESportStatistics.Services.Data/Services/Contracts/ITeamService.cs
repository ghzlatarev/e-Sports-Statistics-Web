using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ITeamService
    {
        IEnumerable<Team> FilterTeams(string filter, int pageNumber, int pageSize);

        Task RebaseTeams(string accessToken);
    }
}
