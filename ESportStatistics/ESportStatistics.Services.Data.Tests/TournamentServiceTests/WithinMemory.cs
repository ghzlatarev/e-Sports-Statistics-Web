using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Context.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.External;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

                result = await SUT.FilterTournamentsAsync(validFilter, validPageNumber, validPageSize);
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
    }
}
