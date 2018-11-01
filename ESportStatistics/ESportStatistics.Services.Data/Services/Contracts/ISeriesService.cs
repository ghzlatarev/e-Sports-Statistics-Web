using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ISeriesService
    {
        IEnumerable<Serie> FilterSeries(string filter, int pageNumber, int pageSize);

        Task RebaseSeries(string accessToken);
    }
}
