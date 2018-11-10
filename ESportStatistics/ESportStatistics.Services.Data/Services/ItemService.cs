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
using X.PagedList;

namespace ESportStatistics.Core.Services
{
    public class ItemService : IItemService
    {
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public ItemService(IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<IPagedList<Item>> FilterItemsAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateNull(filter, "Filter cannot be null!");
            Validator.ValidateNull(sortOrder, "Sort order cannot be null!");

            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = this.dataContext.Items
                .Where(t => t.Name.Contains(filter));

            switch (sortOrder)
            {
                case "name_asc":
                    query = query.OrderBy(u => u.Name);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(u => u.Name);
                    break;
            }

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<Item> FindAsync(string itemId)
        {
            Validator.ValidateNull(itemId, "Item Id cannot be null!");
            Validator.ValidateGuid(itemId, "Item id is not in the correct format.Unable to parse to Guid!");

            var query = await this.dataContext.Items.FindAsync(Guid.Parse(itemId));

            return query;
        }

        public async Task RebaseItemsAsync(string accessToken)
        {
            Validator.ValidateNull(accessToken, "Empty access token!");

            IEnumerable<Item> items = await this.pandaScoreClient
                .GetEntitiesParallel<Item>(accessToken, "items");

            IList<Item> dbItems = await this.dataContext.Items.ToListAsync();

            IList<Item> deleteList = dbItems.Where(i => items.Any(psi => psi.PandaScoreId.Equals(i.PandaScoreId))).ToList();

            this.dataContext.Items.RemoveRange(deleteList);
            await this.dataContext.Items.AddRangeAsync(items);

            await this.dataContext.SaveChangesAsync(false);
        }
    }
}
