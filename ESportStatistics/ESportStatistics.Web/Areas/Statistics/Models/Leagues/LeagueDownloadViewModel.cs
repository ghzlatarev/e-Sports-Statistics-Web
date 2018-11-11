using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Leagues
{
    public class LeagueDownloadViewModel
    {
        public LeagueDownloadViewModel()
        {

        }

        public LeagueDownloadViewModel(League league)
        {
            this.Name = league.Name;
            this.ImageURL = league.ImageURL;
        }

        public string Name { get; set; }

        public string ImageURL { get; set; }
    }
}
