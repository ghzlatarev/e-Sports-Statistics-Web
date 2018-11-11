using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Matches
{
    public class MatchViewModel
    {
        public MatchViewModel()
        {

        }

        public MatchViewModel(Match match)
        {
            this.Name = match.Name;
            this.Id = match.Id.ToString();
        }

        public string Name { get; set; }

        public string Id { get; set; }
    }
}
