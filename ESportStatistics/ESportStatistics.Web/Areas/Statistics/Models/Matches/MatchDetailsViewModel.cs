using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Matches
{
    public class MatchDetailsViewModel
    {

        public MatchDetailsViewModel()
        {
        }

        public MatchDetailsViewModel(Match match)
        {
            this.Name = match.Name;
            this.BeginAt = match.BeginAt;
            this.NumberOfGames = match.NumberOfGames;
            this.MatchType = match.MatchType;
            this.BeginAt = match.BeginAt;
        }

        public string Name { get; set; }
        public string BeginAt { get; set; }
        public int? NumberOfGames { get; set; }
        public string Status { get; set; }
        public string MatchType { get; set; }
    }
}
