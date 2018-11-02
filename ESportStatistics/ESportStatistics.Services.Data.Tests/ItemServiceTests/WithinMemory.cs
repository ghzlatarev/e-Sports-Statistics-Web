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

                result = await SUT.FilterItemsAsync(validFilter, validPageNumber, validPageSize);
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
    }
}
