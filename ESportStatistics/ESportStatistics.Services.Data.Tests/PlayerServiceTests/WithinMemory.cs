using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.External;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ESportStatistics.Services.Data.Tests.PlayerServiceTests
{
    [TestClass]
    public class WithInMemory
    {
        [TestMethod]
        public async Task FilterPlayersAsync_ShouldReturnPlayers_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FilterPlayersAsync_ShouldReturnPlayers_WhenPassedValidParameters")
                .Options;

            string validFilter = "testPlayer";
            int validPageSize = 10;
            int validPageNumber = 1;

            string validName = "testPlayer";
            int validPandaScoreId = 123;
            string hometown = "Dobrich";

            Player validPlayer = new Player
            {
                Id = Guid.NewGuid(),
                Name = validName,
                PandaScoreId = validPandaScoreId,
                Hometown = hometown
            };

            IEnumerable<Player> result = new List<Player>();

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Players.AddAsync(validPlayer);
                await actContext.SaveChangesAsync();

                PlayerService SUT = new PlayerService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FilterPlayersAsync(validFilter, validPageNumber, validPageSize);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                var player = result.ToArray()[0];

                Assert.IsTrue(assertContext.Players.Count().Equals(result.Count()));
                Assert.IsTrue(assertContext.Players.Any(t => t.Name.Equals(player.Name)));
                Assert.IsTrue(assertContext.Players.Any(t => t.Hometown.Equals(player.Hometown)));
            }
        }
    }
}
