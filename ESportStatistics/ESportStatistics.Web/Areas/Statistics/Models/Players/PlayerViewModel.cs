using ESportStatistics.Data.Models;

namespace ESportStatistics.Web.Areas.Statistics.Models.Players
{
    public class PlayerViewModel
    {
        public PlayerViewModel()
        {

        }

        public PlayerViewModel(Player player)
        {
            this.Name = player.Name;
            this.FirstName = player.FirstName;
            this.LastName = player.LastName;
            this.Role = player.Role;
            this.ImageURL = player.ImageURL;
            this.Id = player.Id.ToString();
        }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public string ImageURL { get; set; }

        public string Id { get; set; }
    }
}
