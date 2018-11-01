using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IPlayerService
    {
        IEnumerable<Player> FilterPlayers(string filter, int pageNumber, int pageSize);

        Task RebasePlayers(string accessToken);
    }
}
