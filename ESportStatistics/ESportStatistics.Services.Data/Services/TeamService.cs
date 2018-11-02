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
    public class TeamService : ITeamService
    {
        public TeamService(IDataHandler dataHandler,
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

        public async Task <IEnumerable<Team>> FilterTeamsAsync(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = await this.DataContext.Teams
                .Where(i => i.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task RebaseTeamsAsync(string accessToken)
        {
            IEnumerable<Team> teams = await PandaScoreClient
               .GetEntitiesParallel<Team>(accessToken, "teams");

            IList<Team> dbTeams = await this.DataContext.Teams.ToListAsync();

            IList<Team> deleteList = dbTeams.Where(t => teams.Any(pst => pst.PandaScoreId.Equals(t.PandaScoreId))).ToList();

            this.DataContext.Teams.RemoveRange(deleteList);
            await this.DataContext.Teams.AddRangeAsync(teams);

            await this.DataContext.SaveChangesAsync(false);
        }
    }
}
