using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ITournamentService
    {
        Task <IEnumerable<Tournament>> FilterTournamentsAsync(string filter, int pageNumber, int pageSize);

        Task RebaseTournamentsAsync(string accessToken);
    }
}
