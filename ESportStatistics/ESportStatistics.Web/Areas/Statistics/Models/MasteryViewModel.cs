using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models
{
    public class MasteryViewModel
    {

        public MasteryViewModel()
        {
        }

        public MasteryViewModel(Mastery mastery)
        {
            this.Name = mastery.Name;
        }
        public string Name { get; private set; }
    }
}
