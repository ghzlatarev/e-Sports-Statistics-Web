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
    public class MatchService : IMatchService
    {
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public MatchService(IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<IPagedList<Match>> FilterMatchesAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateNull(filter, "Filter cannot be null!");
            Validator.ValidateNull(sortOrder, "Sort order cannot be null!");

            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = this.dataContext.Matches
                .Where(t => t.Name.Contains(filter));

            switch (sortOrder)
            {
                case "name_asc":
                    query = query.OrderBy(u => u.Name);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(u => u.Name);
                    break;
            }

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<Match> FindAsync(string matchId)
        {
            Validator.ValidateNull(matchId, "Match Id cannot be null!");
            Validator.ValidateGuid(matchId, "Match id is not in the correct format.Unable to parse to Guid!");

            var query = await this.dataContext.Matches.FindAsync(Guid.Parse(matchId));

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
