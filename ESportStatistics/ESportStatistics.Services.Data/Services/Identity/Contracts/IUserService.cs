using ESportStatistics.Data.Models.Identity;
using PagedList.Core;
using System.IO;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Services.Identity.Contracts
{
    public interface IUserService
    {
        IPagedList<ApplicationUser> FilterUsers(string filter = "", int pageNumber = 1, int pageSize = 10);

        Task SaveAvatarImageAsync(Stream stream, string userId);
    }
}

