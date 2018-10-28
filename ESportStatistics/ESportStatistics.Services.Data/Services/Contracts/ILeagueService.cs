using ESportStatistics.Data.Models;
using System.Collections.Generic;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ILeagueService
    {
        IEnumerable<League> FilterLeagues(string filter, int pageNumber, int pageSize);

        void RebaseLeagues();
    }
}
