using ESportStatistics.Data.Models;
using System.Collections.Generic;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ITeamService
    {
        IEnumerable<Team> FilterTeams(string filter, int pageNumber, int pageSize);

        void RebaseTeams();
    }
}
