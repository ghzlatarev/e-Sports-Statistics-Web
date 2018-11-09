using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.External;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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

            Champion validChampion = new Champion
            {
                Name = "testChamp",
                HP = 100,
                MP = 100
            };

            IEnumerable<Champion> result = new List<Champion>();

            // Act
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

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                var champion = result.ToArray()[0];

                Assert.IsTrue(assertContext.Champions.Count().Equals(result.Count()));
                Assert.IsTrue(assertContext.Champions.Any(c => c.Name.Equals(champion.Name)));
                Assert.IsTrue(assertContext.Champions.Any(c => c.HP.Equals(champion.HP)));
                Assert.IsTrue(assertContext.Champions.Any(c => c.MP.Equals(champion.MP)));
            }
        }

        [TestMethod]
        public async Task AddChampionAsync_ShouldAddChampion_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "AddChampionAsync_ShouldAddChampion_WhenPassedValidParameters")
                .Options;

            string validFirstName = "testChamp";
            bool validIsDeleted = false;

            Champion validChampion = new Champion
            {
                Name = validFirstName
            };

            Champion result = null;

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                ChampionService SUT = new ChampionService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.AddChampionAsync(validFirstName);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.CreatedOn);
                Assert.IsNull(result.DeletedOn);
                Assert.IsTrue(result.IsDeleted.Equals(validIsDeleted));
                Assert.IsTrue(assertContext.Champions.Any(c => c.Name.Equals(result.Name)));
            }
        }

        //[TestMethod]
        //public async Task DeleteChampionAsync_ShouldFlagChampionAsDelete_WhenPassedValidParameters()
        //{
        //    // Arrange
        //    var contextOptions = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase(databaseName: "DeleteChampionAsync_ShouldFlagAChampionAsDelete_WhenPassedValidParameters")
        //        .Options;

        //    Guid Id = Guid.NewGuid();
        //    bool validIsDeleted = true;

        //    Champion validChampion = new Champion
        //    {
        //        Id = Id,
        //        Name = "testChamp"
        //    };

        //    Champion result = null;

        //    // Act
        //    using (DataContext actContext = new DataContext(contextOptions))
        //    {
        //        Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

        //        await actContext.AddAsync(validChampion);
        //        await actContext.SaveChangesAsync();

        //        ChampionService SUT = new ChampionService(
        //            pandaScoreClientMock.Object,
        //            actContext);

        //        result = await SUT.DeleteChampionAsync(Id);
        //    }

        //    // Assert
        //    using (DataContext assertContext = new DataContext(contextOptions))
        //    {
        //        Assert.IsNotNull(result);
        //        Assert.IsNotNull(result.DeletedOn);
        //        Assert.IsTrue(result.IsDeleted.Equals(validIsDeleted));
        //    }
        //}

        //[TestMethod]
        //public async Task RestoreChampionAsync_ShouldFlagChampionAsNotDelete_WhenPassedValidParameters()
        //{ // Arrange
        //    var contextOptions = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase(databaseName: "RestoreChampionAsync_ShouldFlagChampionAsNotDelete_WhenPassedValidParameters")
        //        .Options;

        //    Guid Id = Guid.NewGuid();
        //    bool validIsDeleted = false;

        //    Champion validChampion = new Champion
        //    {
        //        Id = Id,
        //        Name = "testChamp",
        //        DeletedOn = DateTime.UtcNow.AddHours(2),
        //        IsDeleted = true
        //    };

        //    Champion result = null;

        //    // Act
        //    using (DataContext actContext = new DataContext(contextOptions))
        //    {
        //        Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

        //        await actContext.AddAsync(validChampion);
        //        await actContext.SaveChangesAsync();

        //        ChampionService SUT = new ChampionService(
        //            pandaScoreClientMock.Object,
        //            actContext);

        //        result = await SUT.RestoreChampionAsync(Id);
        //    }

        //    // Assert
        //    using (DataContext assertContext = new DataContext(contextOptions))
        //    {
        //        Assert.IsNotNull(result);
        //        Assert.IsNull(result.DeletedOn);
        //        Assert.IsTrue(result.IsDeleted.Equals(validIsDeleted));
        //    }
        //}
    }
}
