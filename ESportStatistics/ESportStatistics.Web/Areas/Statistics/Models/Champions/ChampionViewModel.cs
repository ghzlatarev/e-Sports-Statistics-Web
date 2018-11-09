using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Champions
{
    public class ChampionViewModel
    {
        public ChampionViewModel()
        {
        }

        public ChampionViewModel(Champion champion)
        {
            this.Name = champion.Name;
            this.ImageURL = champion.ImageURL;
            this.Id = champion.Id.ToString();
        }

        public string ImageURL { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }
    }
}
