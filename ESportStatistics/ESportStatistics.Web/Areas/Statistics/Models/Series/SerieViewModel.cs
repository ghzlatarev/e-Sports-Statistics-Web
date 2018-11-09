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
            this.Name = serie.Name;
            this.Season = serie.Season;
            this.BeginAt = serie.BeginAt;
            this.Description = serie.Description;
            this.Year = serie.Year;
            this.EndAt = serie.EndAt;
        }

        public string Name { get; set; }

        public string Season { get; set; }

        public DateTime? BeginAt { get; set; }

        public string Description { get; set; }

        public int? Year { get; set; }

        public DateTime? EndAt { get; set; }
    }
}
