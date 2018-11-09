using ESportStatistics.Data.Models;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IChampionService
    {
        Task<IPagedList<Champion>> FilterChampionsAsync(string filter = "", int pageNumber = 1, int pageSize = 10);

        Task<Champion> FindAsync(string id);

        Task<Champion> AddChampionAsync(string name);

        Task<Champion> DeleteChampionAsync(string id);

        Task<Champion> RestoreChampionAsync(string id);

        Task RebaseChampionsAsync(string accessToken);
    }
}
