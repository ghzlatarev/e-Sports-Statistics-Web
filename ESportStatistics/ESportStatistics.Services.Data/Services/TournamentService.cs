using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.External;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESportStatistics.Core.Services
{
    public class TournamentService : ITournamentService
    {
        public TournamentService(IDataHandler dataHandler,
            IPandaScoreClient pandaScoreClient)
        {
            this.DataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
            this.PandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
        }

        private IDataHandler DataHandler { get; }

        private IPandaScoreClient PandaScoreClient { get; }

        public IEnumerable<Tournament> FilterTournaments(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = this.DataHandler.Tournaments.All()
                .Where(t => t.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public void RebaseTournaments()
        {
            throw new NotImplementedException();
            /*IEnumerable<Tournament> tournaments = PandaScoreClient
                .GetEntities<Tournament>(apiUrl)
                .Select(entity => entity as Tournament);

            foreach (var tournament in tournaments)
            {
                var temp = this.DataHandler.Tournaments.All()
                    .SingleOrDefault(t => t.PandaScoreId.Equals(tournament.PandaScoreId));

                if (temp != null)
                {
                    this.DataHandler.Tournaments.Update(temp);
                }
                else
                {
                    this.DataHandler.Tournaments.Add(tournament);
                }
            }

            this.DataHandler.SaveChanges();*/
        }
    }
}
