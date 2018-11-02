using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IMatchService
    {
        Task <IEnumerable<Match>> FilterMatchesAsync(string filter, int pageNumber, int pageSize);

        Task RebaseMatchesAsync(string accessToken);
    }
}
