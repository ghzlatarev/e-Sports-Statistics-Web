using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Tournaments
{
    public class TournamentDownloadViewModel
    {
        public TournamentDownloadViewModel()
        {

        }

        public TournamentDownloadViewModel(Tournament tournament)
        {
            this.Name = tournament.Name;
            this.BeginAt = tournament.BeginAt;
            this.EndAt = tournament.EndAt;
        }

        public string Name { get; set; }

        public string BeginAt { get; set; }

        public string EndAt { get; set; }
    }
}
