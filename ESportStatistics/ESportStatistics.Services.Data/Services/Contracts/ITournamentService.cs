using ESportStatistics.Data.Models;
using System.Collections.Generic;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ITournamentService
    {
        IEnumerable<Tournament> FilterTournaments(string filter, int pageNumber, int pageSize);

        void RebaseTournaments();
    }
}
