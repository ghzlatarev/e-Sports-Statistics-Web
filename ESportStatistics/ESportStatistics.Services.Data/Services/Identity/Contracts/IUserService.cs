using ESportStatistics.Data.Models.Identity;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Services.Identity.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> FilterUsersAsync(string filter = default(string), int pageNumber = 1, int pageSize = 10);

        Task SaveAvatarImageAsync(Stream stream, string userId);
    }
}

