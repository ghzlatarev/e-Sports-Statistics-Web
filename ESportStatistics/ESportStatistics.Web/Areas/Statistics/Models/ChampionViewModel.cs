using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models
{
    public class ChampionViewModel
    {

        public ChampionViewModel()
        {
        }

        public ChampionViewModel(Champion champion)
        { 
       
           this.Name = champion.Name;
           this.Armor = champion.Armor;
        }

        public string Name { get; set; }
        public double? Armor { get; set; }

    }
}
