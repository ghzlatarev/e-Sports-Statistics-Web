using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models
{
    public class ItemViewModel
    {

        public ItemViewModel()
        {
        }

        public ItemViewModel(Item item)
        {
            this.Name = item.Name;
        }

        public string Name { get; private set; }

        //da naredq tuka neshtata za viewmodela
    }
}
