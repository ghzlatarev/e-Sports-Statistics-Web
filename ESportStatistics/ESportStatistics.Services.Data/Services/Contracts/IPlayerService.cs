using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> FilterPlayersAsync(string filter = default(string), int pageNumber = 1, int pageSize = 10);

        Task RebasePlayersAsync(string accessToken);
    }
}
