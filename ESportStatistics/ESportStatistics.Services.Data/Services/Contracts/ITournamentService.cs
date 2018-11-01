using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ITournamentService
    {
        IEnumerable<Tournament> FilterTournaments(string filter, int pageNumber, int pageSize);

        Task RebaseTournaments(string accessToken);
    }
}
