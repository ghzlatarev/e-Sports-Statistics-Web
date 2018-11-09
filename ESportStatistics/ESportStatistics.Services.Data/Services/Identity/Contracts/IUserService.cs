using ESportStatistics.Data.Models.Identity;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Services.Data.Services.Identity.Contracts
{
    public interface IUserService
    {
        Task<IPagedList<ApplicationUser>> FilterUsersAsync(string filter = "", int pageNumber = 1, int pageSize = 10);

        Task<ApplicationUser> DisableUser(string userId);

        Task<ApplicationUser> RestoreUser(string userId);

        Task SaveAvatarImageAsync(Stream stream, string userId);
    }
}

