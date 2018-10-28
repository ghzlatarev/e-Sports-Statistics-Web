using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.External;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESportStatistics.Core.Services
{
    public class TeamService : ITeamService
    {
        public TeamService(IDataHandler dataHandler,
            IPandaScoreClient pandaScoreClient)
        {
            this.DataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
            this.PandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
        }

        private IDataHandler DataHandler { get; }

        private IPandaScoreClient PandaScoreClient { get; }

        public IEnumerable<Team> FilterTeams(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = this.DataHandler.Teams.All()
                .Where(i => i.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public void RebaseTeams()
        {
            throw new NotImplementedException();
            /*IEnumerable<Team> teams = PandaScoreClient
                .GetEntities<Team>(apiUrl)
                .Select(entity => entity as Team);

            foreach (var team in teams)
            {
                var temp = this.DataHandler.Teams.All()
                    .SingleOrDefault(t => t.PandaScoreId.Equals(team.PandaScoreId));

                if (temp != null)
                {
                    this.DataHandler.Teams.Update(temp);
                }
                else
                {
                    this.DataHandler.Teams.Add(team);
                }
            }

            this.DataHandler.SaveChanges();*/
        }
    }
}
