using ESportStatistics.Data.Models;
using System.Collections.Generic;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IMatchService
    {
        IEnumerable<Match> FilterMatches(string filter, int pageNumber, int pageSize);

        void RebaseMatches();
    }
}
