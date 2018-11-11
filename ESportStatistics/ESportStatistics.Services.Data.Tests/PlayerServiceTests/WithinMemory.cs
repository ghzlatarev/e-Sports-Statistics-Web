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

            string validSortOrder = "name_asc";
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

                result = await SUT.FilterPlayersAsync(validSortOrder, validFilter, validPageNumber, validPageSize);
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

        [TestMethod]
        public async Task FindAsync_ShouldReturnPlayer_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FindAsync_ShouldReturnPlayer_WhenPassedValidParameters")
                .Options;

            Guid validId = Guid.NewGuid();

            Player validPlayer = new Player
            {
                Id = validId,
                Name = "testChamp",
                FirstName = "testFirstName",
                LastName = "testLastName"
            };

            Player result = null;

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Players.AddAsync(validPlayer);
                await actContext.SaveChangesAsync();

                PlayerService SUT = new PlayerService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FindAsync(validId.ToString());
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Players.Any(c => c.Id.Equals(result.Id)));
                Assert.IsTrue(assertContext.Players.Any(c => c.Name.Equals(result.Name)));
                Assert.IsTrue(assertContext.Players.Any(c => c.FirstName.Equals(result.FirstName)));
                Assert.IsTrue(assertContext.Players.Any(c => c.LastName.Equals(result.LastName)));
            }
        }

        [TestMethod]
        public async Task RebasePlayersAsync_ShouldRepopulatePlayersTable_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RebasePlayersAsync_ShouldRepopulatePlayersTable_WhenPassedValidParameters")
                .Options;

            string validAccessToken = string.Empty;
            string validCollectionName = "players";
            int validPageSize = 100;

            Player validPlayer = new Player
            {
                Id = Guid.NewGuid(),
                Name = "testPlayer",
                DeletedOn = DateTime.UtcNow.AddHours(2),
                IsDeleted = true
            };

            IEnumerable<Player> validPlayerList = new List<Player>()
            {
                validPlayer
            };

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                pandaScoreClientMock
                    .Setup(mock => mock.GetEntitiesParallel<ESportStatistics.Data.Models.Player>(validAccessToken, validCollectionName, validPageSize))
                    .Returns(Task.FromResult(validPlayerList));

                PlayerService SUT = new PlayerService(
                    pandaScoreClientMock.Object,
                    actContext);

                await SUT.RebasePlayersAsync(validAccessToken);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Players.Count() == 1);
                Assert.IsTrue(assertContext.Players.Contains(validPlayer));
            }
        }
    }
}
