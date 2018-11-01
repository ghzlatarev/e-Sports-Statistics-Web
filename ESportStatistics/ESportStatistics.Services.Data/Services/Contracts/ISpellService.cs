using ESportStatistics.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ISpellService
    {
        IEnumerable<Spell> FilterSpells(string filter, int pageNumber, int pageSize);

        Task RebaseSpells(string accessToken);
    }
}
