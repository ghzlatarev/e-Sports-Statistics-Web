using ESportStatistics.Data.Models;
using System.Collections.Generic;

namespace ESportStatistics.Core.Services.Contracts
{
    public interface ISpellService
    {
        IEnumerable<Spell> FilterSpells(string filter, int pageNumber, int pageSize);

        void RebaseSpells();
    }
}
