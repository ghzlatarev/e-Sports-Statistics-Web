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
            this.ImageURL = team.ImageURL;
            this.Id = team.Id.ToString();
        }

        public string Name { get; set; }

        public string Acronym { get; set; }

        public string ImageURL { get; set; }

        public string Id { get; set; }
    }
}
