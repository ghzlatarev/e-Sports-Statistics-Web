using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Players
{
    public class PlayerDetailsViewModel
    {
        public PlayerDetailsViewModel()
        {

        }

        public PlayerDetailsViewModel(Player player)
        {
            this.Name = player.Name;
            this.FirstName = player.FirstName;
            this.LastName = player.LastName;
            this.Role = player.Role;
            this.Bio = player.Bio;
            this.Hometown = player.Hometown;
            this.ImageURL = player.ImageURL;
        }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public string Bio { get; set; }

        public string Hometown { get; set; }

        public string ImageURL { get; set; }
    }
}
