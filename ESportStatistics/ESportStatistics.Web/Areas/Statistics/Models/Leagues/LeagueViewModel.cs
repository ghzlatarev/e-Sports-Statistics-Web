using ESportStatistics.Data.Models;

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
            this.Id = league.Id.ToString();
        }

        public string Name { get; private set; }

        public string ImageURL { get; private set; }

        public string Id { get; private set; }
    }
}
