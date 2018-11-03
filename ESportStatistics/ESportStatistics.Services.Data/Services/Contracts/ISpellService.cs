using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ISpellService
    {
        Task<IEnumerable<Spell>> FilterSpellsAsync(string filter = default(string), int pageNumber = 1, int pageSize = 10);

        Task RebaseSpellsAsync(string accessToken);
    }
}
