using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
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
        public SpellService(IDataHandler dataHandler,
            IPandaScoreClient pandaScoreClient,
            DataContext dataContext)
        {
            this.DataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
            this.PandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        private IDataHandler DataHandler { get; }

        private IPandaScoreClient PandaScoreClient { get; }

        private DataContext DataContext { get; }

        public async Task <IEnumerable<Spell>> FilterSpellsAsync(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = await this.DataContext.Spells
                .Where(s => s.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task RebaseSpellsAsync(string accessToken)
        {
            IEnumerable<Spell> spells = await PandaScoreClient
               .GetEntitiesParallel<Spell>(accessToken, "spells");

            IList<Spell> dbSpells = await this.DataContext.Spells.ToListAsync();

            IList<Spell> deleteList = dbSpells.Where(s => spells.Any(pss => pss.PandaScoreId.Equals(s.PandaScoreId))).ToList();

            this.DataContext.Spells.RemoveRange(deleteList);
            await this.DataContext.Spells.AddRangeAsync(spells);

            await this.DataContext.SaveChangesAsync(false);
        }
    }
}