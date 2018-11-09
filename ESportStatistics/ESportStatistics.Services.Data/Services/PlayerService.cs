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
    public class PlayerService : IPlayerService
    {
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public PlayerService(IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<Player> FindAsync(string playerId)
        {
            Validator.ValidateNull(playerId, "Player Id cannot be null!");
            Validator.ValidateGuid(playerId, "Player id is not in the correct format.Unable to parse to Guid!");

            var query = await this.dataContext.Players.FindAsync(Guid.Parse(playerId));

            return query;
        }

        public async Task<IPagedList<Player>> FilterPlayersAsync(string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateNull(filter, "Filter cannot be null!");

            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = await this.dataContext.Players
                .Where(t => t.Name.Contains(filter))
                .ToPagedListAsync(pageNumber, pageSize);

            return query;
        }

        public async Task RebasePlayersAsync(string accessToken)
        {
            Validator.ValidateNull(accessToken, "Empty access token!");

            IEnumerable<Player> players = await this.pandaScoreClient
                .GetEntitiesParallel<Player>(accessToken, "players");

            IList<Player> dbPlayers = await this.dataContext.Players.ToListAsync();

            IList<Player> deleteList = dbPlayers.Where(p => players.Any(psp => psp.PandaScoreId.Equals(p.PandaScoreId))).ToList();

            this.dataContext.Players.RemoveRange(deleteList);
            await this.dataContext.Players.AddRangeAsync(players);

            await this.dataContext.SaveChangesAsync(false);
        }
    }
}
