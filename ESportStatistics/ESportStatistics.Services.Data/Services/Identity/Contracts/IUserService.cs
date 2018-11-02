using System.IO;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Services.Identity.Contracts
{
    public interface IUserService
    {
        Task SaveAvatarImageAsync(Stream stream, string userId);
    }
}

