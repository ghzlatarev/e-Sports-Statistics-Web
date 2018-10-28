using ESportStatistics.Data.Models;
using System.Collections.Generic;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IPlayerService
    {
        IEnumerable<Player> FilterPlayers(string filter, int pageNumber, int pageSize);

        void RebasePlayers();
    }
}
