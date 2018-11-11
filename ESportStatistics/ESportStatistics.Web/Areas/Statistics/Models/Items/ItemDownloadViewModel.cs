using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Items
{
    public class ItemDownloadViewModel
    {
        public ItemDownloadViewModel()
        {

        }

        public ItemDownloadViewModel(Item item)
        {
            this.Name = item.Name;
            this.ImageURL = item.ImageURL;
        }

        public string Name { get; set; }

        public string ImageURL { get; set; }
    }
}
