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
    public class ItemService : IItemService
    {
        public ItemService(IDataHandler dataHandler,
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

        public async Task<IEnumerable<Item>> FilterItemsAsync(string filter, int pageNumber, int pageSize)
        {
            var query = await this.DataContext.Items
                .Where(i => i.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task RebaseItemsAsync(string accessToken)
        {
            IEnumerable<Item> items = await PandaScoreClient
                .GetEntitiesParallel<Item>(accessToken, "items");

            IList<Item> dbItems = await this.DataContext.Items.ToListAsync();

            IList<Item> deleteList = dbItems.Where(i => items.Any(psi => psi.PandaScoreId.Equals(i.PandaScoreId))).ToList();

            this.DataContext.Items.RemoveRange(deleteList);
            await this.DataContext.Items.AddRangeAsync(items); 

            await this.DataContext.SaveChangesAsync(false);
        }
    }
}
