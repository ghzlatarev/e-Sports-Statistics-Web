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
using X.PagedList;

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

        public async Task<IPagedList<League>> FilterLeaguesAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateNull(sortOrder, "SortOrder cannot be null!");
            Validator.ValidateNull(filter, "Filter cannot be null!");

            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = this.dataContext.Leagues
                .Where(t => t.Name.Contains(filter));

            switch (sortOrder)
            {
                case "name_asc":
                    query = query.OrderBy(l => l.Name);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(l => l.Name);
                    break;
            }

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<League> FindAsync(string leagueId)
        {
            Validator.ValidateNull(leagueId, "League Id cannot be null!");
            Validator.ValidateGuid(leagueId, "League id is not in the correct format.Unable to parse to Guid!");

            var query = await this.dataContext.Leagues.FindAsync(Guid.Parse(leagueId));

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
