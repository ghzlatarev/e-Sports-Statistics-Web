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
    public class LeagueService : ILeagueService
    {
        public LeagueService(IDataHandler dataHandler,
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

        public IEnumerable<League> FilterLeagues(string filter, int pageNumber, int pageSize)
        {
            var query = this.DataContext.Leagues.AsQueryable()
                .Where(t => t.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public async Task RebaseLeagues(string accessToken)
        {
            IEnumerable<League> leagues = await PandaScoreClient
                .GetEntitiesParallel<League>(accessToken, "leagues");

            IList<League> dbLeagues = await this.DataContext.Leagues.ToListAsync();

            IList<League> deleteList = dbLeagues.Where(l => leagues.Any(psl => psl.PandaScoreId.Equals(l.PandaScoreId))).ToList();

            this.DataContext.Leagues.RemoveRange(deleteList);
            await this.DataContext.Leagues.AddRangeAsync(leagues);

            await this.DataContext.SaveChangesAsync();
        }
    }
}
