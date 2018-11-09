using ESportStatistics.Data.Models;
using System;

namespace ESportStatistics.Web.Areas.Statistics.Models.Series
{
    public class SerieViewModel
    {
        public SerieViewModel()
        {

        }

        public SerieViewModel(Serie serie)
        {
            this.FullName = serie.FullName;
            this.Season = serie.Season;
            this.BeginAt = serie.BeginAt;
            this.EndAt = serie.EndAt;
            this.Id = serie.Id.ToString();
        }

        public string FullName { get; set; }

        public string Season { get; set; }

        public DateTime? BeginAt { get; set; }

        public DateTime? EndAt { get; set; }

        public string Id { get; set; }
    }
}
