using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Teams
{
    public class TeamDetailsViewModel
    {
        public TeamDetailsViewModel()
        {

        }

        public TeamDetailsViewModel(Team team)
        {
            this.Name = team.Name;
            this.Acronym = team.Acronym;
            this.ImageURL = team.ImageURL;
        }

        public string Name { get; set; }

        public string Acronym { get; set; }

        public string ImageURL { get; set; }
    }
}
