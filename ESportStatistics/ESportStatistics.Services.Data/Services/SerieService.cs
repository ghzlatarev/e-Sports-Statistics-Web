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
    public class SerieService : ISerieService
    {
        private readonly DataContext dataContext;
        private readonly IPandaScoreClient pandaScoreClient;

        public SerieService(IPandaScoreClient pandaScoreClient,
            DataContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.pandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
        }

        public async Task<IEnumerable<Serie>> FilterSeriesAsync(string filter, int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = await this.dataContext.Series
                .Where(t => t.Name.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task RebaseSeriesAsync(string accessToken)
        {
            Validator.ValidateNull(accessToken, "Empty access token!");

            IEnumerable<Serie> series = await pandaScoreClient
                .GetEntitiesParallel<Serie>(accessToken, "series");

            IList<Serie> dbPlayers = await this.dataContext.Series.ToListAsync();

            IList<Serie> deleteList = dbPlayers.Where(s => series.Any(pss => pss.PandaScoreId.Equals(s.PandaScoreId))).ToList();

            this.dataContext.Series.RemoveRange(deleteList);
            await this.dataContext.Series.AddRangeAsync(series);

            await this.dataContext.SaveChangesAsync(false);
        }
    }
}
