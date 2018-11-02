using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ISpellService
    {
        Task <IEnumerable<Spell>> FilterSpellsAsync(string filter, int pageNumber, int pageSize);

        Task RebaseSpellsAsync(string accessToken);
    }
}
