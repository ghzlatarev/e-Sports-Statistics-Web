using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.External;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESportStatistics.Core.Services
{
    public class ItemService : IItemService
    {
        public ItemService(IDataHandler dataHandler,
            IPandaScoreClient pandaScoreClient)
        {
            this.DataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
            this.PandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
        }

        private IDataHandler DataHandler { get; }

        private IPandaScoreClient PandaScoreClient { get; }

        public IEnumerable<Item> FilterItems(string filter, int pageNumber, int pageSize)
        {
            var query = this.DataHandler.Items.All()
                .Where(i => i.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public void RebaseItems()
        {
            throw new NotImplementedException();
            /*IEnumerable<Item> items = PandaScoreClient
                .GetEntities<Item>(apiUrl)
                .Select(entity => entity as Item);

            foreach (var item in items)
            {
                var temp = this.DataHandler.Items.All()
                    .SingleOrDefault(i => i.PandaScoreId.Equals(item.PandaScoreId));

                if (temp != null)
                {
                    this.DataHandler.Items.Update(temp);
                }
                else
                {
                    this.DataHandler.Items.Add(item);
                }
            }

            this.DataHandler.SaveChanges();*/
        }
    }
}
