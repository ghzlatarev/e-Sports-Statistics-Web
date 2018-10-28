using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.External;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESportStatistics.Core.Services
{
    public class MatchService : IMatchService
    {
        public MatchService(IDataHandler dataHandler,
            IPandaScoreClient pandaScoreClient)
        {
            this.DataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
            this.PandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
        }

        private IDataHandler DataHandler { get; }

        private IPandaScoreClient PandaScoreClient { get; }

        public IEnumerable<Match> FilterMatches(string filter, int pageNumber, int pageSize)
        {
            var query = this.DataHandler.Matches.All()
                .Where(m => m.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public void RebaseMatches()
        {
            throw new NotImplementedException();
            /*IEnumerable<Match> matches = PandaScoreClient
                .GetEntities<Match>(apiUrl)
                .Select(entity => entity as Match);

            foreach (var match in matches)
            {
                var temp = this.DataHandler.Matches.All()
                    .SingleOrDefault(m => m.PandaScoreId.Equals(match.PandaScoreId));

                if (temp != null)
                {
                    this.DataHandler.Matches.Update(temp);
                }
                else
                {
                    this.DataHandler.Matches.Add(match);
                }
            }

            this.DataHandler.SaveChanges();*/
        }
    }
}
