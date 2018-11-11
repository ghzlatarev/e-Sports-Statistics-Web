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

namespace ESportStatistics.Services.Data.Tests.MasteryServiceTests
{
    [TestClass]
    public class WithInMemory
    {
        [TestMethod]
        public async Task FilterMasteriesAsync_ShouldReturnMasteries_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FilterMasteries_ShouldReturnMasteries_WhenPassedValidParameters")
                .Options;

            string validSortOrder = "name_asc";
            string validFilter = "testMastery";
            int validPageSize = 10;
            int validPageNumber = 1;

            string validName = "testMastery";

            Mastery validMastery = new Mastery
            {
                Name = validName
            };

            IEnumerable<Mastery> result = new List<Mastery>();

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreEndpointrMock = new Mock<IPandaScoreClient>();

                await actContext.Masteries.AddAsync(validMastery);
                await actContext.SaveChangesAsync();

                MasteryService SUT = new MasteryService(
                    pandaScoreEndpointrMock.Object,
                    actContext);

                result = await SUT.FilterMasteriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                var mastery = result.ToArray()[0];

                Assert.IsTrue(assertContext.Masteries.Count().Equals(result.Count()));
                Assert.IsTrue(assertContext.Masteries.Any(m => m.Name.Equals(mastery.Name)));
            }
        }

        [TestMethod]
        public async Task RebaseMasteriesAsync_ShouldRepopulateMasteriesTable_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RebaseMasteriesAsync_ShouldRepopulateMasteriesTable_WhenPassedValidParameters")
                .Options;

            string validAccessToken = string.Empty;
            string validCollectionName = "masteries";
            int validPageSize = 100;

            Mastery validMastery = new Mastery
            {
                Id = Guid.NewGuid(),
                Name = "testMastery",
                DeletedOn = DateTime.UtcNow.AddHours(2),
                IsDeleted = true
            };

            IEnumerable<Mastery> validMasteryList = new List<Mastery>()
            {
                validMastery
            };

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                pandaScoreClientMock
                    .Setup(mock => mock.GetEntitiesParallel<Mastery>(validAccessToken, validCollectionName, validPageSize))
                    .Returns(Task.FromResult(validMasteryList));

                MasteryService SUT = new MasteryService(
                    pandaScoreClientMock.Object,
                    actContext);

                await SUT.RebaseMasteriesAsync(validAccessToken);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Masteries.Count() == 1);
                Assert.IsTrue(assertContext.Masteries.Contains(validMastery));
            }
        }
    }
}
