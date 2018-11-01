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
    public class PlayerService : IPlayerService
    {
        public PlayerService(IDataHandler dataHandler,
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

        public IEnumerable<Player> FilterPlayers(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = this.DataContext.Players.AsQueryable()
                .Where(p => p.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public async Task RebasePlayers(string accessToken)
        {
            IEnumerable<Player> players = await PandaScoreClient
                .GetEntitiesParallel<Player>(accessToken, "players");

            IList<Player> dbPlayers = await this.DataContext.Players.ToListAsync();

            IList<Player> deleteList = dbPlayers.Where(p => players.Any(psp => psp.PandaScoreId.Equals(p.PandaScoreId))).ToList();

            this.DataContext.Players.RemoveRange(deleteList);
            await this.DataContext.Players.AddRangeAsync(players);

            await this.DataContext.SaveChangesAsync();
        }
    }
}
