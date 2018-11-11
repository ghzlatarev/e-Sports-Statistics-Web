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

namespace ESportStatistics.Services.Data.Tests.TournamentServiceTests
{
    [TestClass]
    public class WithInMemory
    {
        [TestMethod]
        public async Task FilterTournamentsAsync_ShouldReturnTournaments_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FilterTournamentsAsync_ShouldReturnTournaments_WhenPassedValidParameters")
                .Options;

            string validSortOrder = "name_asc";
            string validFilter = "testTournament";
            int validPageSize = 10;
            int validPageNumber = 1;

            string validSlug = "Test";
            string validName = "testTournament";
            int validPandaScoreId = 123;

            Tournament validTournament = new Tournament
            {
                Id = Guid.NewGuid(),
                Name = validName,
                PandaScoreId = validPandaScoreId,
                Slug = validSlug
            };

            IEnumerable<Tournament> result = new List<Tournament>();

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Tournaments.AddAsync(validTournament);
                await actContext.SaveChangesAsync();

                TournamentService SUT = new TournamentService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FilterTournamentsAsync(validSortOrder, validFilter, validPageNumber, validPageSize);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                var tournament = result.ToArray()[0];

                Assert.IsTrue(assertContext.Tournaments.Count().Equals(result.Count()));
                Assert.IsTrue(assertContext.Tournaments.Any(t => t.Name.Equals(tournament.Name)));
                Assert.IsTrue(assertContext.Tournaments.Any(t => t.Slug.Equals(tournament.Slug)));
            }
        }

        [TestMethod]
        public async Task FindAsync_ShouldReturnTournament_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FindAsync_ShouldReturnTournament_WhenPassedValidParameters")
                .Options;

            Guid validId = Guid.NewGuid();

            Tournament validTournament = new Tournament
            {
                Id = validId,
                Name = "testTournament"
            };

            Tournament result = null;

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Tournaments.AddAsync(validTournament);
                await actContext.SaveChangesAsync();

                TournamentService SUT = new TournamentService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FindAsync(validId.ToString());
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Tournaments.Any(c => c.Id.Equals(result.Id)));
                Assert.IsTrue(assertContext.Tournaments.Any(c => c.Name.Equals(result.Name)));
            }
        }

        [TestMethod]
        public async Task RebaseTournamentsAsync_ShouldRepopulateTournamentTable_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RebaseTournamentsAsync_ShouldRepopulateTournamentsTable_WhenPassedValidParameters")
                .Options;

            string validAccessToken = string.Empty;
            string validCollectionName = "tournaments";
            int validPageSize = 100;

            Tournament validTournament = new Tournament
            {
                Id = Guid.NewGuid(),
                Name = "testTournament",
                DeletedOn = DateTime.UtcNow.AddHours(2),
                IsDeleted = true
            };

            IEnumerable<Tournament> validTournamentList = new List<Tournament>()
            {
                validTournament
            };

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                pandaScoreClientMock
                    .Setup(mock => mock.GetEntitiesParallel<Tournament>(validAccessToken, validCollectionName, validPageSize))
                    .Returns(Task.FromResult(validTournamentList));

                TournamentService SUT = new TournamentService(
                    pandaScoreClientMock.Object,
                    actContext);

                await SUT.RebaseTournamentsAsync(validAccessToken);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Tournaments.Count() == 1);
                Assert.IsTrue(assertContext.Tournaments.Contains(validTournament));
            }
        }
    }
}
