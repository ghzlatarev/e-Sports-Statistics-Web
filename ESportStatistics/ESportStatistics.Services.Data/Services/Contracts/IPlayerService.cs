using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IPlayerService
    {
        Task<IPagedList<Player>> FilterPlayersAsync(string filter = "", int pageNumber = 1, int pageSize = 10);

        Task RebasePlayersAsync(string accessToken);
    }
}
