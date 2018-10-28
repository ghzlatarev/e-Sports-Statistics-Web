using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.External;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESportStatistics.Core.Services
{
    public class MasteryService : IMasteryService
    {
        public MasteryService(IDataHandler dataHandler,
            IPandaScoreClient pandaScoreClient)
        {
            this.DataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
            this.PandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
        }

        private IDataHandler DataHandler { get; }

        private IPandaScoreClient PandaScoreClient { get; }

        public IEnumerable<Mastery> FilterMasteries(string filter, int pageNumber, int pageSize)
        {
            var query = this.DataHandler.Masteries.All()
                .Where(m => m.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public void RebaseMasteries()
        {
            throw new NotImplementedException();
            /*IEnumerable<Mastery> masteries = PandaScoreClient
                .GetEntities<Mastery>(apiUrl)
                .Select(entity => entity as Mastery);

            foreach (var mastery in masteries)
            {
                var temp = this.DataHandler.Masteries.All()
                    .SingleOrDefault(m => m.PandaScoreId.Equals(mastery.PandaScoreId));

                if (temp != null)
                {
                    this.DataHandler.Masteries.Update(temp);
                }
                else
                {
                    this.DataHandler.Masteries.Add(mastery);
                }
            }

            this.DataHandler.SaveChanges();*/
        }
    }
}
