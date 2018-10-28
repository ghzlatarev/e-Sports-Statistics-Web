using ESportStatistics.Data.Models;
using System.Collections.Generic;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ISeriesService
    {
        IEnumerable<Serie> FilterSeries(string filter, int pageNumber, int pageSize);

        void RebaseSeries();
    }
}
