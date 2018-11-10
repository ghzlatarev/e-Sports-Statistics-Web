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

namespace ESportStatistics.Services.Data.Tests.ItemServiceTests
{
    [TestClass]
    public class WithInMemory
    {
        [TestMethod]
        public async Task FilterItemsAsync_ShouldReturnItems_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ItemService_ShouldReturnItems_WhenPassedValidParameters")
                .Options;

            string validSortOrder = "name_asc";
            string validFilter = "testItem";
            int validPageSize = 10;
            int validPageNumber = 1;

            string validFirstName = "testItem";
            int validTotalGold = 100;
            int validSellGold = 50;

            Item validItem = new Item
            {
                Name = validFirstName,
                TotalGold = validTotalGold,
                SellGold = validSellGold
            };

            IEnumerable<Item> result = new List<Item>();

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Items.AddAsync(validItem);
                await actContext.SaveChangesAsync();

                ItemService SUT = new ItemService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FilterItemsAsync(validSortOrder, validFilter, validPageNumber, validPageSize);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                var item = result.ToArray()[0];

                Assert.IsTrue(assertContext.Items.Count().Equals(result.Count()));
                Assert.IsTrue(assertContext.Items.Any(i => i.Name.Equals(item.Name)));
                Assert.IsTrue(assertContext.Items.Any(i => i.TotalGold.Equals(item.TotalGold)));
                Assert.IsTrue(assertContext.Items.Any(i => i.SellGold.Equals(item.SellGold)));
            }
        }

        [TestMethod]
        public async Task FindAsync_ShouldReturnPlayer_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FindAsync_ShouldReturnPlayer_WhenPassedValidParameters")
                .Options;

            Guid validId = Guid.NewGuid();

            Item validItem = new Item
            {
                Id = validId,
                Name = "testName",
                TotalGold = It.IsAny<int>(),
                BaseGold = It.IsAny<int>()
            };

            Item result = null;

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Items.AddAsync(validItem);
                await actContext.SaveChangesAsync();

                ItemService SUT = new ItemService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FindAsync(validId.ToString());
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Items.Any(c => c.Id.Equals(result.Id)));
                Assert.IsTrue(assertContext.Items.Any(c => c.Name.Equals(result.Name)));
                Assert.IsTrue(assertContext.Items.Any(c => c.TotalGold.Equals(result.TotalGold)));
                Assert.IsTrue(assertContext.Items.Any(c => c.BaseGold.Equals(result.BaseGold)));
            }
        }

        [TestMethod]
        public async Task RebaseItemsAsync_ShouldRepopulateChampionTable_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RebaseItemsAsync_ShouldRepopulateChampionTable_WhenPassedValidParameters")
                .Options;

            string validAccessToken = string.Empty;
            string validCollectionName = "items";
            int validPageSize = 100;

            Item validItem = new Item
            {
                Id = Guid.NewGuid(),
                Name = "testChamp",
                DeletedOn = DateTime.UtcNow.AddHours(2),
                IsDeleted = true
            };

            IEnumerable<Item> validItemList = new List<Item>()
            {
                validItem
            };

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                pandaScoreClientMock
                    .Setup(mock => mock.GetEntitiesParallel<Item>(validAccessToken, validCollectionName, validPageSize))
                    .Returns(Task.FromResult(validItemList));

                ItemService SUT = new ItemService(
                    pandaScoreClientMock.Object,
                    actContext);

                await SUT.RebaseItemsAsync(validAccessToken);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Items.Count() == 1);
                Assert.IsTrue(assertContext.Items.Contains(validItem));
            }
        }
    }
}
