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

namespace ESportStatistics.Services.Data.Tests.ChampionServiceTests
{
    [TestClass]
    public class WithInMemory
    {
        [TestMethod]
        public async Task FilterChampionsAsync_ShouldReturnChampions_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ChampionService_ShouldReturnChampions_WhenPassedValidParameters")
                .Options;

            string validFilter = "testChamp";
            int validPageSize = 10;
            int validPageNumber = 1;

            string validFirstName = "testChamp";
            int validHP = 100;
            int validMP = 100;

            Champion validChampion = new Champion
            {
                Name = validFirstName,
                HP = validHP,
                MP = validMP
            };

            IEnumerable<Champion> result = new List<Champion>();

            // Act & Assert
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Champions.AddAsync(validChampion);
                await actContext.SaveChangesAsync();

                ChampionService SUT = new ChampionService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FilterChampionsAsync(validFilter, validPageNumber, validPageSize);
            }

            using (DataContext assertContext = new DataContext(contextOptions))
            {
                var champion = result.ToArray()[0];

                Assert.IsTrue(assertContext.Champions.Count().Equals(result.Count()));
                Assert.IsTrue(assertContext.Champions.Any(c => c.Name.Equals(champion.Name)));
                Assert.IsTrue(assertContext.Champions.Any(c => c.HP.Equals(champion.HP)));
                Assert.IsTrue(assertContext.Champions.Any(c => c.MP.Equals(champion.MP)));
            }
        }
    }
}
