using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Data.Utils;
using ESportStatistics.Services.External;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Core.Services
{
    public class SpellService : ISpellService
    {
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public SpellService(IPandaScoreClient pandaScoreClient,
            DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<Spell> FindAsync(string spellId)
        {
            Validator.ValidateNull(spellId, "Spell Id cannot be null!");
            Validator.ValidateGuid(spellId, "Spell id is not in the correct format.Unable to parse to Guid!");

            var query = await this.dataContext.Spells.FindAsync(Guid.Parse(spellId));

            return query;
        }

        public async Task<IPagedList<Spell>> FilterSpellsAsync(string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateNull(filter, "Filter cannot be null!");

            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = await this.dataContext.Spells
                .Where(t => t.Name.Contains(filter))
                .ToPagedListAsync(pageNumber, pageSize);

            return query;
        }

        public async Task RebaseSpellsAsync(string accessToken)
        {
            Validator.ValidateNull(accessToken, "Empty access token!");

            IEnumerable<Spell> spells = await this.pandaScoreClient
               .GetEntitiesParallel<Spell>(accessToken, "spells");

            IList<Spell> dbSpells = await this.dataContext.Spells.ToListAsync();

            IList<Spell> deleteList = dbSpells.Where(s => spells.Any(pss => pss.PandaScoreId.Equals(s.PandaScoreId))).ToList();

            this.dataContext.Spells.RemoveRange(deleteList);
            await this.dataContext.Spells.AddRangeAsync(spells);

            await this.dataContext.SaveChangesAsync(false);
        }
    }
}