using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Series
{
    public class SerieDownloadViewModel
    {
        public SerieDownloadViewModel()
        {

        }

        public SerieDownloadViewModel(Serie serie)
        {
            this.FullName = serie.FullName;
            this.Season = serie.Season;
            this.BeginAt = serie.BeginAt;
            this.EndAt = serie.EndAt;
        }

        public string FullName { get; set; }

        public string Season { get; set; }

        public DateTime? BeginAt { get; set; }

        public DateTime? EndAt { get; set; }
    }
}
