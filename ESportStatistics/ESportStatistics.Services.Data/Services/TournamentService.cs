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
    public class TournamentService : ITournamentService
    {
        public TournamentService(IDataHandler dataHandler,
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

        public IEnumerable<Tournament> FilterTournaments(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = this.DataContext.Tournaments.AsQueryable()
                .Where(t => t.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public async Task RebaseTournaments(string accessToken)
        {
            IEnumerable<Tournament> tournaments = await PandaScoreClient
               .GetEntitiesParallel<Tournament>(accessToken, "tournaments");

            IList<Tournament> dbTournaments = await this.DataContext.Tournaments.ToListAsync();

            IList<Tournament> deleteList = dbTournaments.Where(t => tournaments.Any(pst => pst.PandaScoreId.Equals(t.PandaScoreId))).ToList();

            this.DataContext.Tournaments.RemoveRange(deleteList);
            await this.DataContext.Tournaments.AddRangeAsync(tournaments);

            await this.DataContext.SaveChangesAsync();
        }
    }
}
