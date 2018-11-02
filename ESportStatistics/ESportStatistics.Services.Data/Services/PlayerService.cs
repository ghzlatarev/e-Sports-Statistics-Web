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
    public class PlayerService : IPlayerService
    {
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public PlayerService(IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task <IEnumerable<Player>> FilterPlayersAsync(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = await this.dataContext.Players
                .Where(p => p.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

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
