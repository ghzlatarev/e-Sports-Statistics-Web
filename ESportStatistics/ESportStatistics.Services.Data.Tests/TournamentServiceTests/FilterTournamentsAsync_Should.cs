using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Services.External;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ESportStatistics.Services.Data.Tests.TournamentServiceTests
{
    [TestClass]
    public class FilterTournamentsAsync_Should
    {
        [TestMethod]
        public async Task ThrowException_WhenPassedInvalidPageSize()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            string validFilter = "testTournament";
            int invalidPageSize = -1;
            int validPageNumber = 1;

            TournamentService SUT = new TournamentService(
                    pandaScoreClientMock.Object,
                    dataContextMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(
                    () => SUT.FilterTournamentsAsync(validFilter, validPageNumber, invalidPageSize));
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedInvalidPageNumber()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            string validFilter = "testTournament";
            int validPageSize = 10;
            int invalidPageNumber = -1;

            TournamentService SUT = new TournamentService(
                    pandaScoreClientMock.Object,
                    dataContextMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(
                    () => SUT.FilterTournamentsAsync(validFilter, invalidPageNumber, validPageSize));
        }
    }
}
