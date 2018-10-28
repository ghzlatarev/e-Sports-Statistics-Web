using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.External;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESportStatistics.Core.Services
{
    public class LeagueService : ILeagueService
    {
        public LeagueService(IDataHandler dataHandler,
            IPandaScoreClient pandaScoreClient)
        {
            this.DataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
            this.PandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
        }

        private IDataHandler DataHandler { get; }

        private IPandaScoreClient PandaScoreClient { get; }

        public IEnumerable<League> FilterLeagues(string filter, int pageNumber, int pageSize)
        {
            var query = this.DataHandler.Leagues.All()
                .Where(t => t.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public void RebaseLeagues()
        {
            throw new NotImplementedException();
            /*IEnumerable<League> leagues = PandaScoreClient
                .GetEntities<League>(apiUrl)
                .Select(entity => entity as League);

            foreach (var league in leagues)
            {
                var temp = this.DataHandler.Leagues.All()
                    .SingleOrDefault(l => l.PandaScoreId.Equals(league.PandaScoreId));

                if (temp != null)
                {
                    this.DataHandler.Leagues.Update(temp);
                }
                else
                {
                    this.DataHandler.Leagues.Add(league);
                }
            }

            this.DataHandler.SaveChanges();*/
        }
    }
}
