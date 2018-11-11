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
    public class MasteryService : IMasteryService
    {
        private readonly IPandaScoreClient pandaScoreClient;
        private readonly DataContext dataContext;

        public MasteryService(IPandaScoreClient pandaScoreClient, DataContext dataContext)
        {
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<IPagedList<Mastery>> FilterMasteriesAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateNull(filter, "Filter cannot be null!");
            Validator.ValidateNull(sortOrder, "Sort order cannot be null!");

            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = this.dataContext.Masteries
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
