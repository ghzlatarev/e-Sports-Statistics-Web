using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Data.Exceptions;
using ESportStatistics.Services.Data.Utils;
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
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public ChampionService(IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<IEnumerable<Champion>> FilterChampionsAsync(string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = await this.dataContext.Champions
                .Where(t => t.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task<IEnumerable<Champion>> ReturnChampionAsync(Guid Id)
        {
            var query = await this.dataContext.Champions
                .Where(t => t.Id.Equals(Id))
                .ToListAsync();

            return query;
        }

        public async Task<Champion> AddChampionAsync(string name)
        {
            Validator.ValidateNull(name, "Champion name cannot be null!");

            if (await this.dataContext.Champions.AnyAsync(t => t.Name.Equals(name)))
            {
                throw new EntityAlreadyExistsException("Champion already exists!");
            }

            var champion = new Champion
            {
                Name = name
            };

            await this.dataContext.Champions.AddAsync(champion);
            await this.dataContext.SaveChangesAsync();

            return champion;
        }

        public async Task<Champion> DeleteChampionAsync(Guid Id)
        {
            Validator.ValidateNull(Id, "Champion Id cannot be null!");

            var champion = await this.dataContext.Champions.FindAsync(Id);

            Validator.ValidateNull(champion, "Invalid champion!");

            if (champion.IsDeleted == true)
            {
                throw new EntityAlreadyDeletedException();
            }

            this.dataContext.Champions.Remove(champion);
            await this.dataContext.SaveChangesAsync();

            return champion;
        }

        public async Task<Champion> RestoreChampionAsync(Guid Id)
        {
            Validator.ValidateNull(Id, "Champion Id cannot be null!");

            var champion = await this.dataContext.Champions.FindAsync(Id);

            Validator.ValidateNull(champion, "Invalid champion!");

            if (champion.IsDeleted == false)
            {
                throw new EntityAlreadyActiveException();
            }

            champion.IsDeleted = false;
            champion.DeletedOn = null;

            this.dataContext.Champions.Update(champion);
            await this.dataContext.SaveChangesAsync();

            return champion;
        }

        public async Task RebaseChampionsAsync(string accessToken)
        {
            Validator.ValidateNull(accessToken, "Empty access token!");

            IEnumerable<Champion> champions = await this.pandaScoreClient
                .GetEntitiesParallel<Champion>(accessToken, "champions");

            IList<Champion> dbChampions = await this.dataContext.Champions.ToListAsync();

            IList<Champion> deleteList = dbChampions.Where(c => champions.Any(psc => psc.PandaScoreId.Equals(c.PandaScoreId))).ToList();

            this.dataContext.Champions.RemoveRange(deleteList);
            await this.dataContext.Champions.AddRangeAsync(champions);

            await this.dataContext.SaveChangesAsync(false);
        }
    }
}
