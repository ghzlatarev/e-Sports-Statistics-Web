using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models
{
    public class TournamentViewModel
    {

        public TournamentViewModel()
        {
        }

        public TournamentViewModel(Tournament tournament)
        {
            this.Name = tournament.Name;
            this.Slug = tournament.Slug;
            this.BeginAt = tournament.BeginAt;
            this.EndAt = tournament.EndAt;
        }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string BeginAt { get; set; }
        public string EndAt { get; set; }
    }
}
