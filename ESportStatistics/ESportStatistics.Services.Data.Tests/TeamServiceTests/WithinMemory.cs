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

namespace ESportStatistics.Services.Data.Tests.TeamServiceTests
{
    [TestClass]
    public class WithInMemory
    {
        [TestMethod]
        public async Task FilterTeamsAsync_ShouldReturnTeams_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FilterTeamsAsync_ShouldReturnTeams_WhenPassedValidParameters")
                .Options;

            string validSortOrder = "name_asc";
            string validFilter = "testTeam";
            int validPageSize = 10;
            int validPageNumber = 1;

            string validAcronym = "Test";
            string validName = "testTeam";
            int validPandaScoreId = 123;

            Team validTeam = new Team
            {
                Id = Guid.NewGuid(),
                Name = validName,
                PandaScoreId = validPandaScoreId,
                Acronym = validAcronym
            };

            IEnumerable<Team> result = new List<Team>();

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Teams.AddAsync(validTeam);
                await actContext.SaveChangesAsync();

                TeamService SUT = new TeamService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FilterTeamsAsync(validSortOrder, validFilter, validPageNumber, validPageSize);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                var team = result.ToArray()[0];

                Assert.IsTrue(assertContext.Teams.Count().Equals(result.Count()));
                Assert.IsTrue(assertContext.Teams.Any(t => t.Name.Equals(team.Name)));
                Assert.IsTrue(assertContext.Teams.Any(t => t.Acronym.Equals(team.Acronym)));
            }
        }

        [TestMethod]
        public async Task FindAsync_ShouldReturnTeam_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FindAsync_ShouldReturnTeam_WhenPassedValidParameters")
                .Options;

            Guid validId = Guid.NewGuid();

            Team validTeam = new Team
            {
                Id = validId,
                Name = "testTeam"
            };

            Team result = null;

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Teams.AddAsync(validTeam);
                await actContext.SaveChangesAsync();

                TeamService SUT = new TeamService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FindAsync(validId.ToString());
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Teams.Any(c => c.Id.Equals(result.Id)));
                Assert.IsTrue(assertContext.Teams.Any(c => c.Name.Equals(result.Name)));
            }
        }

        [TestMethod]
        public async Task RebaseTeamsAsync_ShouldRepopulateTeamTable_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RebaseTeamsAsync_ShouldRepopulateTeamTable_WhenPassedValidParameters")
                .Options;

            string validAccessToken = string.Empty;
            string validCollectionName = "teams";
            int validPageSize = 100;

            Team validTeam = new Team
            {
                Id = Guid.NewGuid(),
                Name = "testTeam",
                DeletedOn = DateTime.UtcNow.AddHours(2),
                IsDeleted = true
            };

            IEnumerable<Team> validTeamList = new List<Team>()
            {
                validTeam
            };

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                pandaScoreClientMock
                    .Setup(mock => mock.GetEntitiesParallel<Team>(validAccessToken, validCollectionName, validPageSize))
                    .Returns(Task.FromResult(validTeamList));

                TeamService SUT = new TeamService(
                    pandaScoreClientMock.Object,
                    actContext);

                await SUT.RebaseTeamsAsync(validAccessToken);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Teams.Count() == 1);
                Assert.IsTrue(assertContext.Teams.Contains(validTeam));
            }
        }
    }
}
