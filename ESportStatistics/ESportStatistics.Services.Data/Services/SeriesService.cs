using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using ESportStatistics.Services.External;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESportStatistics.Core.Services
{
    public class SeriesService : ISeriesService
    {
        public SeriesService(IDataHandler dataHandler,
            IPandaScoreClient pandaScoreClient)
        {
            this.DataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
            this.PandaScoreClient = pandaScoreClient ?? throw new ArgumentNullException(nameof(pandaScoreClient));
        }

        private IDataHandler DataHandler { get; }

        private IPandaScoreClient PandaScoreClient { get; }

        public IEnumerable<Serie> FilterSeries(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var query = this.DataHandler.Series.All()
                .Where(t => t.Name.Contains(filter)
                ).Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return query;
        }

        public void RebaseSeries()
        {
            throw new NotImplementedException();
            /*IEnumerable<Serie> series = PandaScoreEndpoint
                .GetEntities<Serie>(apiUrl)
                .Select(entity => entity as Serie);

            foreach (var serie in series)
            {
                var temp = this.DataHandler.Series.All()
                    .SingleOrDefault(s => s.PandaScoreId.Equals(serie.PandaScoreId));

                if (temp != null)
                {
                    this.DataHandler.Series.Update(temp);
                }
                else
                {
                    this.DataHandler.Series.Add(serie);
                }
            }

            this.DataHandler.SaveChanges();*/
        }
    }
}
