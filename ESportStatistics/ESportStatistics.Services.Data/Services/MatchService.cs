using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Context.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.Data.Utils;
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
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public MatchService( IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<IEnumerable<Match>> FilterMatchesAsync(string filter, int pageNumber, int pageSize)
        {
            var query = await this.dataContext.Matches
                .Where(m => m.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task RebaseMatchesAsync(string accessToken)
        {
            Validator.ValidateNull(accessToken, "Empty access token!");

            IEnumerable<Match> matches = await this.pandaScoreClient
                .GetEntitiesParallel<Match>(accessToken, "matches");

            IList<Match> dbMatches = await this.dataContext.Matches.ToListAsync();

            IList<Match> deleteList = dbMatches.Where(m => matches.Any(psm => psm.PandaScoreId.Equals(m.PandaScoreId))).ToList();

            this.dataContext.Matches.RemoveRange(deleteList);
            await this.dataContext.Matches.AddRangeAsync(matches);

            await this.dataContext.SaveChangesAsync(false);
        }
    }
}
