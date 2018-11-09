using ESportStatistics.Data.Models;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ISerieService
    {
        Task<IPagedList<Serie>> FilterSeriesAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10);

        Task RebaseSeriesAsync(string accessToken);

        Task<Serie> FindAsync(string id);
    }
}
