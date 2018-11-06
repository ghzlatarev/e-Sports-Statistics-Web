using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ISerieService
    {
        Task<IEnumerable<Serie>> FilterSeriesAsync(string filter = "", int pageNumber = 1, int pageSize = 10);

        Task RebaseSeriesAsync(string accessToken);
    }
}
