using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface IMasteryService
    {
        IEnumerable<Mastery> FilterMasteries(string filter, int pageNumber, int pageSize);

        Task RebaseMasteries(string accessToken);
    }
}
