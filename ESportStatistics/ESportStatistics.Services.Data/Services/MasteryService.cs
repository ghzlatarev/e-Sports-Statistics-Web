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
    public class MasteryService : IMasteryService
    {
        public MasteryService(IDataHandler dataHandler,
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

        public async Task<IEnumerable<Mastery>> FilterMasteriesAsync(string filter, int pageNumber, int pageSize)
        {
            var query = await this.DataContext.Masteries
                .Where(m => m.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task RebaseMasteriesAsync(string accessToken)
        {
            IEnumerable<Mastery> masteries = await PandaScoreClient
                .GetEntitiesParallel<Mastery>(accessToken, "masteries");

            IList<Mastery> dbMasteries = await this.DataContext.Masteries.ToListAsync();

            IList<Mastery> deleteList = dbMasteries.Where(m => masteries.Any(psm => psm.PandaScoreId.Equals(m.PandaScoreId))).ToList();

            this.DataContext.Masteries.RemoveRange(deleteList);
            await this.DataContext.Masteries.AddRangeAsync(masteries);

            await this.DataContext.SaveChangesAsync(false);
        }
    }
}
