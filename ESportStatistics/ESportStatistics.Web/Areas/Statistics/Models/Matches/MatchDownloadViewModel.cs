using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Matches
{
    public class MatchDownloadViewModel
    {
        public MatchDownloadViewModel()
        {

        }

        public MatchDownloadViewModel(Match match)
        {
            this.Name = match.Name;
        }

        public string Name { get; set; }
    }
}
