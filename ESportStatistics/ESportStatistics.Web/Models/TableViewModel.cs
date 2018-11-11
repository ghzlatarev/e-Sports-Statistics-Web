using System.Collections.Generic;

namespace ESportStatistics.Web.Models
{
    public class TableViewModel<T> where T : class
    {
        public TableViewModel()
        {

        }

        public IEnumerable<T> Items { get; set; }

        public string StatusMessage { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}
