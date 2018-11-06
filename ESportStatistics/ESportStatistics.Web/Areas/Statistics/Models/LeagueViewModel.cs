using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models
{
    public class LeagueViewModel
    {

        public LeagueViewModel()
        {
        }

        public LeagueViewModel(League league)
        {
            this.Name = league.Name;
        }
        public string Name { get; private set; }

    }
}
