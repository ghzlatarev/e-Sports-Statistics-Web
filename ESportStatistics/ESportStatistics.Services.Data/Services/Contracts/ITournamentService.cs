using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ITournamentService
    {
        Task<IPagedList<Tournament>> FilterTournamentsAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10);

        Task RebaseTournamentsAsync(string accessToken);

        Task<Tournament> FindAsync(string id);
    }
}
