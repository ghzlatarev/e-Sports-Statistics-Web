using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ILeagueService
    {
        IEnumerable<League> FilterLeagues(string filter, int pageNumber, int pageSize);

        Task RebaseLeagues(string accessToken);
    }
}
