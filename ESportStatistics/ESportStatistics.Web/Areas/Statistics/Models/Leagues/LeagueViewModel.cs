using ESportStatistics.Data.Models;
using System;

namespace ESportStatistics.Web.Areas.Statistics.Models.Leagues
{
    public class LeagueViewModel
    {

        public LeagueViewModel()
        {
        }

        public LeagueViewModel(League league)
        {
            this.Name = league.Name;
            this.ImageURL = league.ImageURL;
            this.Id = league.Id;
        }

        public string Name { get; private set; }
        public string ImageURL { get; private set; }
        public Guid Id { get; private set; }

    }
}
