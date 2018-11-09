using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Teams
{
    public class TeamViewModel
    {
        public TeamViewModel()
        {

        }

        public TeamViewModel(Team team)
        {
            this.Name = team.Name;
            this.Acronym = team.Acronym;
        }

        public string Name { get; set; }

        public string Acronym { get; set; }
    }
}
