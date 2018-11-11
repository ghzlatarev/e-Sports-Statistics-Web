using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Services.External;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Tests.LeagueServiceTests
{
    [TestClass]
    public class FilterLeaguesAsync_Should
    {
        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedNullSortOrder()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            string invalidSortOrder = null;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            LeagueService SUT = new LeagueService(
                pandaScoreClientMock.Object,
                dataContextMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => SUT.FilterLeaguesAsync(invalidSortOrder, validFilter, validPageNumber, validPageSize));
        }

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedNullFilter()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            string validSortOrder = string.Empty;
            string invalidFilter = null;
            int validPageNumber = 1;
            int validPageSize = 10;

            LeagueService SUT = new LeagueService(
                pandaScoreClientMock.Object,
                dataContextMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => SUT.FilterLeaguesAsync(validSortOrder, invalidFilter, validPageNumber, validPageSize));
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-10)]
        public async Task ThrowArgumentOutOfRangeException_WhenPassedInvalidPageNumber(int invalidPageNumber)
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageSize = 10;

            LeagueService SUT = new LeagueService(
                pandaScoreClientMock.Object,
                dataContextMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
                () => SUT.FilterLeaguesAsync(validSortOrder, validFilter, invalidPageNumber, validPageSize));
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-10)]
        public async Task ThrowArgumentOutOfRangeException_WhenPassedInvalidPageSize(int invalidPageSize)
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;

            LeagueService SUT = new LeagueService(
                pandaScoreClientMock.Object,
                dataContextMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
                () => SUT.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, invalidPageSize));
        }
    }
}
