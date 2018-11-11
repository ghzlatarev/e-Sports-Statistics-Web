using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Masteries
{
    public class MasteryDownloadViewModel
    {
        public MasteryDownloadViewModel()
        {

        }

        public MasteryDownloadViewModel(Mastery mastery)
        {
            this.Name = mastery.Name;
        }

        public string Name { get; set; }
    }
}
