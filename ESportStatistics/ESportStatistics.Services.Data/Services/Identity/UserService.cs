using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models.Identity;
using ESportStatistics.Services.Data.Exceptions;
using ESportStatistics.Services.Data.Services.Identity.Contracts;
using ESportStatistics.Services.Data.Utils;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Services.Data.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly DataContext dataContext;

        public UserService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IPagedList<ApplicationUser>> FilterUsersAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateNull(filter, "Filter cannot be null!");
            Validator.ValidateNull(sortOrder, "SortOrder cannot be null!");

            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = this.dataContext.Users
                .Where(u => u.UserName.Contains(filter) || u.Email.Contains(filter));

            switch (sortOrder)
            {
                case "username_asc":
                    query = query.OrderBy(u => u.UserName);
                    break;
                case "username_desc":
                    query = query.OrderByDescending(u => u.UserName);
                    break;
                case "email_asc":
                    query = query.OrderBy(u => u.Email);
                    break;
                case "email_desc":
                    query = query.OrderByDescending(u => u.Email);
                    break;
            }

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<ApplicationUser> DisableUser(string userId)
        {
            Validator.ValidateNull(userId, "User Id cannot be null!");
            Validator.ValidateGuid(userId, "User id is not in the correct format.Unable to parse to Guid!");

            ApplicationUser user = await this.dataContext.Users.FindAsync(userId);

            if (userId == null)
            {
                throw new EntityNotFoundException();
            }

            this.dataContext.Remove(user);
            await this.dataContext.SaveChangesAsync();

            return user;
        }

        public async Task<ApplicationUser> RestoreUser(string userId)
        {
            Validator.ValidateNull(userId, "User Id cannot be null!");
            Validator.ValidateGuid(userId, "User id is not in the correct format.Unable to parse to Guid!");

            ApplicationUser user = await this.dataContext.Users.FindAsync(userId);

            if (userId == null)
            {
                throw new EntityNotFoundException();
            }

            user.IsDeleted = false;
            user.DeletedOn = null;

            await this.dataContext.SaveChangesAsync();

            return user;
        }

        public async Task SaveAvatarImageAsync(Stream stream, string userId)
        {
            Validator.ValidateNull(stream, "Image stream cannot be null!");
            Validator.ValidateNull(userId, "User Id cannot be null!");
            Validator.ValidateGuid(userId, "User id is not in the correct format.Unable to parse to Guid!");

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
