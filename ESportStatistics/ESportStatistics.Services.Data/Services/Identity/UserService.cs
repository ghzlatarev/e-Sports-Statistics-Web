using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models.Identity;
using ESportStatistics.Services.Data.Exceptions;
using ESportStatistics.Services.Data.Services.Identity.Contracts;
using ESportStatistics.Services.Data.Utils;
using System.IO;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly DataContext context;

        public UserService(DataContext context)
        {
            this.context = context;
        }

        public async Task SaveAvatarImageAsync(Stream stream, string userId)
        {
            Validator.ValidateNull(stream, "Image stream cannot be null!");
            Validator.ValidateNull(userId, "User Id cannot be null!");

            ApplicationUser user = await this.context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            using (BinaryReader br = new BinaryReader(stream))
            {
                user.AvatarImage = br.ReadBytes((int)stream.Length);
            }

            await this.context.SaveChangesAsync();
        }
    }
}
