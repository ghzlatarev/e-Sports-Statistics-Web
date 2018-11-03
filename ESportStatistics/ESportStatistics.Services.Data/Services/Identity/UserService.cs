using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models.Identity;
using ESportStatistics.Services.Data.Exceptions;
using ESportStatistics.Services.Data.Services.Identity.Contracts;
using ESportStatistics.Services.Data.Utils;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly DataContext dataContext;

        public UserService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IEnumerable<ApplicationUser>> FilterUsersAsync(string filter = default(string), int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = await this.dataContext.Users
                .Where(t => t.UserName.Contains(filter) || t.Email.Contains(filter))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return query;
        }

        public async Task SaveAvatarImageAsync(Stream stream, string userId)
        {
            Validator.ValidateNull(stream, "Image stream cannot be null!");
            Validator.ValidateNull(userId, "User Id cannot be null!");

            ApplicationUser user = await this.dataContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            using (BinaryReader br = new BinaryReader(stream))
            {
                user.AvatarImage = br.ReadBytes((int)stream.Length);
            }

            await this.dataContext.SaveChangesAsync();
        }
    }
}
