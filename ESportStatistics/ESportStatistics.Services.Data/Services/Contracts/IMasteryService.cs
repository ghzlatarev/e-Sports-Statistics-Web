using ESportStatistics.Data.Models;
using System.Collections.Generic;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IMasteryService
    {
        IEnumerable<Mastery> FilterMasteries(string filter, int pageNumber, int pageSize);

        void RebaseMasteries();
    }
}
