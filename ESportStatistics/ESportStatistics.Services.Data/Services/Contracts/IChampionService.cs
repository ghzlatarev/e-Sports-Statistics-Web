using ESportStatistics.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IChampionService
    {
        Task<IEnumerable<Champion>> FilterChampionsAsync(string filter = "", int pageNumber = 1, int pageSize = 10);

        Task<IEnumerable<Champion>> ReturnChampionAsync(Guid id);

        Task<Champion> AddChampionAsync(string name);

        Task<Champion> DeleteChampionAsync(Guid Id);

        Task<Champion> RestoreChampionAsync(Guid Id);

        Task RebaseChampionsAsync(string accessToken);
    }
}
