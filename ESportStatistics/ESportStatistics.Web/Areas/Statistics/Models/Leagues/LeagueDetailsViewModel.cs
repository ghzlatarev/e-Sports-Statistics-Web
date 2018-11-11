using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Leagues
{
    public class LeagueDetailsViewModel
    {
        public LeagueDetailsViewModel()
        {

        }

        public LeagueDetailsViewModel(League league)
        {
            this.Name = league.Name;
            this.URL = league.URL;
            this.Slug = league.Slug;
            this.ImageURL = league.ImageURL;
        }

        public string URL { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string ImageURL { get; set; }
    }
}
