﻿using ESportStatistics.Core.Services.Contracts;
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
    public class ItemService : IItemService
    {
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public ItemService(IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<IEnumerable<Item>> FilterItemsAsync(string filter = default(string), int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = await this.dataContext.Items
                .Where(i => i.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task<Item> ReturnItemsAsync(Guid Id)
        {
            var query = await this.dataContext.Items
                .Where(t => t.Id.Equals(Id)).FirstAsync();

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
