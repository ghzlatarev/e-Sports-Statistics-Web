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
    public class SeriesService : ISeriesService
    {
        public SeriesService(IDataHandler dataHandler,
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

        public IEnumerable<Serie> FilterSeries(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = this.DataContext.Series.AsQueryable()
                .Where(t => t.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public async Task RebaseSeries(string accessToken)
        {
            IEnumerable<Serie> series = await PandaScoreClient
                .GetEntitiesParallel<Serie>(accessToken, "series");

            IList<Serie> dbPlayers = await this.DataContext.Series.ToListAsync();

            IList<Serie> deleteList = dbPlayers.Where(s => series.Any(pss => pss.PandaScoreId.Equals(s.PandaScoreId))).ToList();

            this.DataContext.Series.RemoveRange(deleteList);
            await this.DataContext.Series.AddRangeAsync(series);

            await this.DataContext.SaveChangesAsync();
        }
    }
}
