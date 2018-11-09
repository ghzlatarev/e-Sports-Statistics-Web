using ESportStatistics.Data.Models;
using System;

namespace ESportStatistics.Web.Areas.Statistics.Models.Matches
{
    public class MatchViewModel
    {

        public MatchViewModel()
        {
        }

        public MatchViewModel(Match match)
        {
            this.Name = match.Name;
            this.Id = match.Id;
        }

        public string Name { get; private set; }

        public Guid Id { get; private set; }
    }
}
