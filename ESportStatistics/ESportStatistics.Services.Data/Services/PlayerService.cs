using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.External;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESportStatistics.Core.Services
{
    public class PlayerService : IPlayerService
    {
        public PlayerService(IDataHandler dataHandler,
            IPandaScoreClient pandaScoreClient)
        {
            this.DataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
            this.PandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
        }

        private IDataHandler DataHandler { get; }

        private IPandaScoreClient PandaScoreClient { get; }

        public IEnumerable<Player> FilterPlayers(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = this.DataHandler.Players.All()
                .Where(p => p.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public void RebasePlayers()
        {
            throw new NotImplementedException();
            /*IEnumerable<Player> players = PandaScoreClient
                .GetEntities<Player>(apiUrl)
                .Select(entity => entity as Player);

            foreach (var player in players)
            {
                var temp = this.DataHandler.Players.All().SingleOrDefault(p => p.PandaScoreId.Equals(player.PandaScoreId));

                if (temp != null)
                {
                    this.DataHandler.Players.Update(temp);
                }
                else
                {
                    this.DataHandler.Players.Add(player);
                }
            }

            this.DataHandler.SaveChanges();*/
        }
    }
}
