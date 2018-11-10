using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.External;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Tests.LeagueServiceTests
{
    [TestClass]
    public class WithInMemory
    {
        [TestMethod]
        public async Task FilterItemsAsync_ShouldReturnItems_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "LeagueService_ShouldReturnLeagues_WhenPassedValidParameters")
                .Options;

            string validSortOrder = "name_asc";
            string validFilter = "testLeague";
            int validPageSize = 10;
            int validPageNumber = 1;

            string validFirstName = "testLeague";
            string validSlug = "validSlug";
            bool validLifeSupported = true;

            League validLeague = new League
            {
                Name = validFirstName,
                Slug = validSlug,
                LifeSupported = validLifeSupported
            };

            IEnumerable<League> result = new List<League>();

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Leagues.AddAsync(validLeague);
                await actContext.SaveChangesAsync();

                LeagueService SUT = new LeagueService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize);

                Assert.IsTrue(result.Count() == 1);
                Assert.IsTrue(result.ToArray()[0].Name.Equals(validFirstName));
                Assert.IsTrue(result.ToArray()[0].Slug == validSlug);
                Assert.IsTrue(result.ToArray()[0].LifeSupported == true);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                var league = result.ToArray()[0];

                Assert.IsTrue(assertContext.Leagues.Count().Equals(result.Count()));
                Assert.IsTrue(assertContext.Leagues.Any(l => l.Name.Equals(league.Name)));
                Assert.IsTrue(assertContext.Leagues.Any(l => l.Slug.Equals(league.Slug)));
                Assert.IsTrue(assertContext.Leagues.Any(l => l.LifeSupported.Equals(league.LifeSupported)));
            }
        }
    }
}
