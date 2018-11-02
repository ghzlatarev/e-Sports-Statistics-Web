using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IChampionService
    {
        Task<IEnumerable<Champion>> FilterChampionsAsync(string filter, int pageNumber = 1, int pageSize = 10);

        Task<Champion> AddChampionAsync(string name);

        Task<Champion> DeleteChampionAsync(string name);

        Task<Champion> RestoreChampionAsync(string name);

        Task RebaseChampionsAsync(string accessToken);
    }
}
