using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IChampionService
    {
        IEnumerable<Champion> FilterChampions(string filter, int pageNumber = 1, int pageSize = 10);

        Champion AddChampion(string name);

        Champion DeleteChampion(string name);

        Champion RestoreChampion(string name);

        Task RebaseChampions(string accessToken);
    }
}
