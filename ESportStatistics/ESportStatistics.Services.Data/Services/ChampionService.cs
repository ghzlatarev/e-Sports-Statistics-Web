using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.Data.Exceptions;
using ESportStatistics.Services.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services
{
    public class ChampionService : IChampionService
    {
        public ChampionService(IDataHandler dataHandler,
            IPandaScoreClient pandaScoreClient)
        {
            this.DataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
            this.PandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
        }

        private IDataHandler DataHandler { get; }

        private IPandaScoreClient PandaScoreClient { get; }

        public IEnumerable<Champion> FilterChampions(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = this.DataHandler.Champions.All()
                .Where(t => t.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public Champion AddChampion(string name)
        {
            if (this.DataHandler.Champions.All().Any(
                t => t.Name.Equals(name)))
            {
                throw new EntityAlreadyExistsException("Champion already exists!");
            }

            var champion = new Champion
            {
                Name = name
            };

            this.DataHandler.Champions.Add(champion);
            this.DataHandler.SaveChanges();

            return champion;
        }

        public Champion DeleteChampion(string name)
        {
            var champion = this.DataHandler.Champions.AllWithDeleted()
                .SingleOrDefault(c => c.Name.Equals(name));

            if (!champion.IsDeleted)
            {
                this.DataHandler.Champions.Delete(champion);
                this.DataHandler.SaveChanges();
            }

            return champion;
        }

        public Champion RestoreChampion(string name)
        {
            var champion = this.DataHandler.Champions.AllWithDeleted()
                .SingleOrDefault(c => c.Name.Equals(name));

            if (champion.IsDeleted)
            {
                champion.IsDeleted = false;
                champion.DeletedOn = null;

                this.DataHandler.Champions.Update(champion);
                this.DataHandler.SaveChanges();
            }

            return champion;
        }

        public async Task RebaseChampions(string accessToken)
        {
            IEnumerable<Champion> champions = await PandaScoreClient
                .GetEntitiesParallel<Champion>(accessToken, "champions");

            foreach (var champion in champions)
            {
                var temp = (await this.DataHandler.Champions.AllAsync())
                    .FirstOrDefault(c => c.PandaScoreId.Equals(champion.PandaScoreId));

                if (temp != null)
                {
                    this.DataHandler.Champions.HardDelete(temp);
                }

                await this.DataHandler.Champions.AddAsync(champion);
            }

            await this.DataHandler.SaveChangesAsync();
        }
    }
}
