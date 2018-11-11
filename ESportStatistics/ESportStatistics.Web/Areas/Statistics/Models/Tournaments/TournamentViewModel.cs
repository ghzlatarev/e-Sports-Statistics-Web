using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Tournaments
{
    public class TournamentViewModel
    {
        public TournamentViewModel()
        {

        }

        public TournamentViewModel(Tournament tournament)
        {
            this.Name = tournament.Name;
            this.BeginAt = tournament.BeginAt;
            this.EndAt = tournament.EndAt;
            this.Id = tournament.Id.ToString();
        }

        public string Name { get; set; }

        public string BeginAt { get; set; }

        public string EndAt { get; set; }

        public string Id { get; set; }
    }
}
