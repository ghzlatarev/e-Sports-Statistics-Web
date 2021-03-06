﻿using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Services.External;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Tests.MatchServiceTests
{
    [TestClass]
    public class WithInMemory
    {
        [TestMethod]
        public async Task FilterMatchesAsync_ShouldReturnMatches_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FilterMatches_ShouldReturnMatches_WhenPassedValidParameters")
                .Options;

            string validSortOrder = "name_asc";
            string validFilter = "testMatch";
            int validPageSize = 10;
            int validPageNumber = 1;

            string validName = "testMatch";
            int validPandaScoreId = 123;
            int validNumberOfGames = 3;
            string validBeginAt = DateTime.Now.ToString();

            ESportStatistics.Data.Models.Match validMatch = new ESportStatistics.Data.Models.Match
            {
                Id = Guid.NewGuid(),
                Name = validName,
                PandaScoreId = validPandaScoreId,
                NumberOfGames = validNumberOfGames,
                BeginAt = validBeginAt
            };

            IEnumerable<ESportStatistics.Data.Models.Match> result = new List<ESportStatistics.Data.Models.Match>();

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Matches.AddAsync(validMatch);
                await actContext.SaveChangesAsync();

                MatchService SUT = new MatchService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                var match = result.ToArray()[0];

                Assert.IsTrue(assertContext.Matches.Count().Equals(result.Count()));
                Assert.IsTrue(assertContext.Matches.Any(m => m.Name.Equals(match.Name)));
                Assert.IsTrue(assertContext.Matches.Any(m => m.NumberOfGames.Equals(match.NumberOfGames)));
                Assert.IsTrue(assertContext.Matches.Any(m => m.BeginAt.Equals(match.BeginAt)));
            }
        }

        [TestMethod]
        public async Task RebaseMatchesAsync_ShouldRepopulateMatchesTable_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RebaseMatchesAsync_ShouldRepopulateMatchesTable_WhenPassedValidParameters")
                .Options;

            string validAccessToken = string.Empty;
            string validCollectionName = "matches";
            int validPageSize = 100;

            ESportStatistics.Data.Models.Match validMatch = new ESportStatistics.Data.Models.Match
            {
                Id = Guid.NewGuid(),
                Name = "testMatch",
                DeletedOn = DateTime.UtcNow.AddHours(2),
                IsDeleted = true
            };

            IEnumerable<ESportStatistics.Data.Models.Match> validMatchList = new List<ESportStatistics.Data.Models.Match>()
            {
                validMatch
            };

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                pandaScoreClientMock
                    .Setup(mock => mock.GetEntitiesParallel<ESportStatistics.Data.Models.Match>(validAccessToken, validCollectionName, validPageSize))
                    .Returns(Task.FromResult(validMatchList));

                MatchService SUT = new MatchService(
                    pandaScoreClientMock.Object,
                    actContext);

                await SUT.RebaseMatchesAsync(validAccessToken);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Matches.Count() == 1);
                Assert.IsTrue(assertContext.Matches.Contains(validMatch));
            }
        }

        [TestMethod]
        public async Task FindAsync_ShouldReturnMatches_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FindAsync_ShouldReturnMatches_WhenPassedValidParameters")
                .Options;

            Guid validId = Guid.NewGuid();

            ESportStatistics.Data.Models.Match validMatch = new ESportStatistics.Data.Models.Match
            {
                Id = validId,
                Name = "testMatch"
            };

            ESportStatistics.Data.Models.Match result = null;

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Matches.AddAsync(validMatch);
                await actContext.SaveChangesAsync();

                MatchService SUT = new MatchService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FindAsync(validId.ToString());
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Matches.Any(c => c.Id.Equals(result.Id)));
                Assert.IsTrue(assertContext.Matches.Any(c => c.Name.Equals(result.Name)));
            }
        }
    }
}
