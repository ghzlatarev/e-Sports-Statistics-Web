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
    public class TournamentService : ITournamentService
    {
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public TournamentService(IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<Tournament> FindAsync(string tournamentId)
        {
            Validator.ValidateNull(tournamentId, "Tournament Id cannot be null!");
            Validator.ValidateGuid(tournamentId, "Tournament id is not in the correct format.Unable to parse to Guid!");

            var query = await this.dataContext.Tournaments.FindAsync(Guid.Parse(tournamentId));

            return query;
        }

        public async Task<IPagedList<Tournament>> FilterTournamentsAsync(string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateNull(filter, "Filter cannot be null!");

            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = await this.dataContext.Tournaments
                .Where(t => t.Name.Contains(filter))
                .ToPagedListAsync(pageNumber, pageSize);

            return query;
        }

        public async Task RebaseTournamentsAsync(string accessToken)
        {
            Validator.ValidateNull(accessToken, "Empty access token!");

            IEnumerable<Tournament> tournaments = await this.pandaScoreClient
               .GetEntitiesParallel<Tournament>(accessToken, "tournaments");

            IList<Tournament> dbTournaments = await this.dataContext.Tournaments.ToListAsync();

            IList<Tournament> deleteList = dbTournaments.Where(t => tournaments.Any(pst => pst.PandaScoreId.Equals(t.PandaScoreId))).ToList();

            this.dataContext.Tournaments.RemoveRange(deleteList);
            await this.dataContext.Tournaments.AddRangeAsync(tournaments);

            await this.dataContext.SaveChangesAsync(false);
        }
    }
}
