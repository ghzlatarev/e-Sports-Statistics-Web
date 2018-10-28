using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.External;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESportStatistics.Core.Services
{
    public class SpellService : ISpellService
    {
        public SpellService(IDataHandler dataHandler,
            IPandaScoreClient pandaScoreClient)
        {
            this.DataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
            this.PandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
        }

        private IDataHandler DataHandler { get; }

        private IPandaScoreClient PandaScoreClient { get; }

        public IEnumerable<Spell> FilterSpells(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = this.DataHandler.Spells.All()
                .Where(s => s.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public void RebaseSpells()
        {
            throw new NotImplementedException();
            /*IEnumerable<Spell> spells = PandaScoreEndpoint
                .GetEntities<Spell>(apiUrl)
                .Select(entity => entity as Spell);

            foreach (var spell in spells)
            {
                var temp = this.DataHandler.Spells.All()
                    .SingleOrDefault(s => s.PandaScoreId.Equals(spell.PandaScoreId));

                if (temp != null)
                {
                    this.DataHandler.Spells.Update(temp);
                }
                else
                {
                    this.DataHandler.Spells.Add(spell);
                }
            }

            this.DataHandler.SaveChanges();*/
        }
    }
}