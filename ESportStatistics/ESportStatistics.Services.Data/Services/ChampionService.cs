using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.Data.Exceptions;
using ESportStatistics.Services.External;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Services
{
    public class ChampionService : IChampionService
    {
        public ChampionService(IDataHandler dataHandler,
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

        public IEnumerable<Champion> FilterChampions(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = this.DataContext.Champions.AsQueryable()
                .Where(t => t.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public Champion AddChampion(string name)
        {
            if (this.DataContext.Champions.AsQueryable().Any(
                t => t.Name.Equals(name)))
            {
                throw new EntityAlreadyExistsException("Champion already exists!");
            }

            var champion = new Champion
            {
                Name = name
            };

            this.DataContext.Champions.Add(champion);
            this.DataContext.SaveChanges();

            return champion;
        }

        public Champion DeleteChampion(string name)
        {
            var champion = this.DataContext.Champions.AsQueryable()
                .SingleOrDefault(c => c.Name.Equals(name));

            if (!champion.IsDeleted)
            {
                this.DataContext.Champions.Remove(champion);
                this.DataContext.SaveChanges();
            }

            return champion;
        }

        public Champion RestoreChampion(string name)
        {
            var champion = this.DataContext.Champions.AsQueryable()
                .SingleOrDefault(c => c.Name.Equals(name));

            if (champion.IsDeleted)
            {
                champion.IsDeleted = false;
                champion.DeletedOn = null;

                this.DataContext.Champions.Update(champion);
                this.DataContext.SaveChanges();
            }

            return champion;
        }

        public async Task RebaseChampions(string accessToken)
        {
            IEnumerable<Champion> champions = await PandaScoreClient
                .GetEntitiesParallel<Champion>(accessToken, "champions");

            IList<Champion> dbChampions = await this.DataContext.Champions.ToListAsync();

            IList<Champion> deleteList = dbChampions.Where(c => champions.Any(psc => psc.PandaScoreId.Equals(c.PandaScoreId))).ToList();

            this.DataContext.Champions.RemoveRange(deleteList);
            await this.DataContext.Champions.AddRangeAsync(champions);

            await this.DataContext.SaveChangesAsync(false);
        }
    }
}
