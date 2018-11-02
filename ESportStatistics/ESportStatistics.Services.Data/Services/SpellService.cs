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

        public async Task<IEnumerable<Spell>> FilterSpellsAsync(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = await this.dataContext.Spells
                .Where(s => s.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

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