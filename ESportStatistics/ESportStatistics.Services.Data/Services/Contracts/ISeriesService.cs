using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ISeriesService
    {
        Task <IEnumerable<Serie>> FilterSeriesAsync(string filter, int pageNumber, int pageSize);

        Task RebaseSeriesAsync(string accessToken);
    }
}
