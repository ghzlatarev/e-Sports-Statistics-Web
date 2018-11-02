using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IPlayerService
    {
        Task <IEnumerable<Player>> FilterPlayersAsync(string filter, int pageNumber, int pageSize);

        Task RebasePlayersAsync(string accessToken);
    }
}
