using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Spells
{
    public class SpellDetailsViewModel
    {
        public SpellDetailsViewModel()
        {
        }

        public SpellDetailsViewModel(Spell spell)
        {
            this.Name = spell.Name;
            this.ImageURL = spell.ImageURL;
        }

        public string Name { get; set; }

        public string ImageURL { get; set; }
    }
}
