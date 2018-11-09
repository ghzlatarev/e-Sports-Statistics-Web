using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ISpellService
    {
        Task<IPagedList<Spell>> FilterSpellsAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10);

        Task RebaseSpellsAsync(string accessToken);

        Task<Spell> FindAsync(string id);
    }
}
