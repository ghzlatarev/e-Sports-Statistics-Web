using ESportStatistics.Data.Models;
using System;

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
            this.Id = item.Id;

        }

        public string Name { get; private set; }
        public string ImageURL { get; private set; }
        public Guid Id { get; private set; }



        //da naredq tuka neshtata za viewmodela
    }
}
