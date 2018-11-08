using ESportStatistics.Data.Models.Identity;
using PagedList.Core;
using System.IO;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Services.Identity.Contracts
{
    public interface IUserService
    {
        Task<ApplicationUser> FindAsync(string userId);

        IPagedList<ApplicationUser> FilterUsers(string filter = "", int pageNumber = 1, int pageSize = 10);

        Task<ApplicationUser> DisableUser(string userId);

        Task<ApplicationUser> RestoreUser(string userId);

        Task SaveAvatarImageAsync(Stream stream, string userId);
    }
}

