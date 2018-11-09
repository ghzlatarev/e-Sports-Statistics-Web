using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Series
{
    public class SerieDetailsViewModel
    {
        public SerieDetailsViewModel()
        {

        }

        public SerieDetailsViewModel(Serie serie)
        {
            this.Season = serie.Season;
            this.Description = serie.Description;
            this.Year = serie.Year;
            this.BeginAt = serie.BeginAt;
            this.EndAt = serie.EndAt;
            this.FullName = serie.FullName;
            this.Slug = serie.Slug;
        }

        public string Season { get; set; }

        public string Description { get; set; }

        public int? Year { get; set; }

        public DateTime? BeginAt { get; set; }

        public DateTime? EndAt { get; set; }

        public string FullName { get; set; }

        public string Slug { get; set; }
    }
}
