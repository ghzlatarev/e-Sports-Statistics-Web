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
    public class LeagueService : ILeagueService
    {
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public LeagueService(IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<IEnumerable<League>> FilterLeaguesAsync(string filter, int pageNumber, int pageSize)
        {
            var query = await this.dataContext.Leagues
                .Where(t => t.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task RebaseLeaguesAsync(string accessToken)
        {
            Validator.ValidateNull(accessToken, "Empty access token!");

            IEnumerable<League> leagues = await this.pandaScoreClient
                .GetEntitiesParallel<League>(accessToken, "leagues");

            IList<League> dbLeagues = await this.dataContext.Leagues.ToListAsync();

            IList<League> deleteList = dbLeagues.Where(l => leagues.Any(psl => psl.PandaScoreId.Equals(l.PandaScoreId))).ToList();

            this.dataContext.Leagues.RemoveRange(deleteList);
            await this.dataContext.Leagues.AddRangeAsync(leagues);

            await this.dataContext.SaveChangesAsync(false);
        }
    }
}
