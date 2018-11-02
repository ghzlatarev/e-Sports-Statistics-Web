using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESportStatistics.Services.Data.Tests.UserServiceTests
{
    [TestClass]
    public class WithinMemory
    {
        [TestMethod]
        public async Task SaveAvatarImageAsync_ShouldReturnTournaments_WhenPassedValidParameters()
        {

        }

        //public async Task SaveAvatarImageAsync(Stream stream, string userId)
        //{
        //    ApplicationUser user = await this.context.Users.FindAsync(userId);

        //    using (BinaryReader br = new BinaryReader(stream))
        //    {
        //        user.AvatarImage = br.ReadBytes((int)stream.Length);
        //    }

        //    await this.context.SaveChangesAsync();
        //}

    }
}
