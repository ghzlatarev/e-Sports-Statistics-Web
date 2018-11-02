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

        public async Task<IEnumerable<Champion>> FilterChampionsAsync(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = await this.DataContext.Champions
                .Where(t => t.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task<Champion> AddChampionAsync(string name)
        {
            if (await this.DataContext.Champions.AnyAsync(t => t.Name.Equals(name)))
            {
                throw new EntityAlreadyExistsException("Champion already exists!");
            }

            var champion = new Champion
            {
                Name = name
            };

            this.DataContext.Champions.Add(champion);
            await this.DataContext.SaveChangesAsync();

            return champion;
        }

        public async Task<Champion> DeleteChampionAsync(string name)
        {
            var champion = await this.DataContext.Champions
                .SingleOrDefaultAsync(c => c.Name.Equals(name));

            if (champion.IsDeleted == false)
            {
                this.DataContext.Champions.Remove(champion);
                await this.DataContext.SaveChangesAsync();
            }

            return champion;
        }

        public async Task<Champion> RestoreChampionAsync(string name)
        {
            var champion = await this.DataContext.Champions
                .SingleOrDefaultAsync(c => c.Name.Equals(name));

            if (champion.IsDeleted)
            {
                champion.IsDeleted = false;
                champion.DeletedOn = null;

                this.DataContext.Champions.Update(champion);
                await this.DataContext.SaveChangesAsync();
            }

            return champion;
        }

        public async Task RebaseChampionsAsync(string accessToken)
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
