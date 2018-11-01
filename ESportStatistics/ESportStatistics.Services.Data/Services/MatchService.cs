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
    public class MatchService : IMatchService
    {
        public MatchService(IDataHandler dataHandler,
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

        public IEnumerable<Match> FilterMatches(string filter, int pageNumber, int pageSize)
        {
            var query = this.DataContext.Matches.AsQueryable()
                .Where(m => m.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public async Task RebaseMatches(string accessToken)
        {
            IEnumerable<Match> matches = await PandaScoreClient
                .GetEntitiesParallel<Match>(accessToken, "matches");

            IList<Match> dbMatches = await this.DataContext.Matches.ToListAsync();

            IList<Match> deleteList = dbMatches.Where(m => matches.Any(psm => psm.PandaScoreId.Equals(m.PandaScoreId))).ToList();

            this.DataContext.Matches.RemoveRange(deleteList);
            await this.DataContext.Matches.AddRangeAsync(matches);

            await this.DataContext.SaveChangesAsync();
        }
    }
}
