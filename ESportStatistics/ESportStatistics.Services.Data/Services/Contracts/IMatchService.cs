using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IMatchService
    {
        IEnumerable<Match> FilterMatches(string filter, int pageNumber, int pageSize);

        Task RebaseMatches(string accessToken);
    }
}
