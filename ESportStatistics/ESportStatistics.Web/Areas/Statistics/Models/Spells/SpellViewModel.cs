using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models
{
    public class SpellViewModel
    {
        public SpellViewModel()
        {
        }

        public SpellViewModel(Spell spell)
        {
            this.Name = spell.Name;
            this.ImageURL = spell.ImageURL;
        }

        public string Name { get; set; }
        public string ImageURL { get; set; }
    }
}
