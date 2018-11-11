using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Items
{
    public class ItemViewModel
    {
        public ItemViewModel()
        {
        }

        public ItemViewModel(Item item)
        {
            this.Name = item.Name;
            this.ImageURL = item.ImageURL;
            this.Id = item.Id.ToString();
        }

        public string Name { get; private set; }

        public string ImageURL { get; private set; }

        public string Id { get; private set; }
    }
}
