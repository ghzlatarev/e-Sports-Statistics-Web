using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Champions
{
    public class ChampionDownloadViewModel
    {
        public ChampionDownloadViewModel()
        {

        }

        public ChampionDownloadViewModel(Champion champion)
        {
            ImageURL = champion.ImageURL;
            Name = champion.Name;
        }

        public string ImageURL { get; set; }

        public string Name { get; set; }
    }
}
