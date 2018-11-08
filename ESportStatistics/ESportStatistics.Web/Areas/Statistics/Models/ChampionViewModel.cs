using ESportStatistics.Data.Models;
using System;

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
            this.ImageURL = champion.ImageURL;
            this.Id = champion.Id;
        }

        public string ImageURL { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }

    }
}
