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
    public class TeamService : ITeamService
    {
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public TeamService(IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<Team> FindAsync(string teamId)
        {
            Validator.ValidateNull(teamId, "Team Id cannot be null!");
            Validator.ValidateGuid(teamId, "Team id is not in the correct format.Unable to parse to Guid!");

            var query = await this.dataContext.Teams.FindAsync(Guid.Parse(teamId));

            return query;
        }

        public async Task<IPagedList<Team>> FilterTeamsAsync(string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateNull(filter, "Filter cannot be null!");

            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = await this.dataContext.Teams
                .Where(t => t.Name.Contains(filter))
                .ToPagedListAsync(pageNumber, pageSize);

            return query;
        }

        public async Task RebaseTeamsAsync(string accessToken)
        {
            Validator.ValidateNull(accessToken, "Empty access token!");

            IEnumerable<Team> teams = await this.pandaScoreClient
               .GetEntitiesParallel<Team>(accessToken, "teams");

            IList<Team> dbTeams = await this.dataContext.Teams.ToListAsync();

            IList<Team> deleteList = dbTeams.Where(t => teams.Any(pst => pst.PandaScoreId.Equals(t.PandaScoreId))).ToList();

            this.dataContext.Teams.RemoveRange(deleteList);
            await this.dataContext.Teams.AddRangeAsync(teams);

            await this.dataContext.SaveChangesAsync(false);
        }
    }
}
