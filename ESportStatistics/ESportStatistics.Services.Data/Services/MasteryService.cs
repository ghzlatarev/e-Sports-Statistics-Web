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

namespace ESportStatistics.Core.Services
{
    public class MasteryService : IMasteryService
    {
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public MasteryService(IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<IEnumerable<Mastery>> FilterMasteriesAsync(string filter, int pageNumber, int pageSize)
        {
            var query = await this.dataContext.Masteries
                .Where(m => m.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task RebaseMasteriesAsync(string accessToken)
        {
            Validator.ValidateNull(accessToken, "Empty access token!");

            IEnumerable<Mastery> masteries = await this.pandaScoreClient
                .GetEntitiesParallel<Mastery>(accessToken, "masteries");

            IList<Mastery> dbMasteries = await this.dataContext.Masteries.ToListAsync();

            IList<Mastery> deleteList = dbMasteries.Where(m => masteries.Any(psm => psm.PandaScoreId.Equals(m.PandaScoreId))).ToList();

            this.dataContext.Masteries.RemoveRange(deleteList);
            await this.dataContext.Masteries.AddRangeAsync(masteries);

            await this.dataContext.SaveChangesAsync(false);
        }
    }
}
