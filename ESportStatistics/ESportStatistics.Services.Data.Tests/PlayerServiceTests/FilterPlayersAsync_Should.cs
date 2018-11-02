using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Services.External;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ESportStatistics.Services.Data.Tests.PlayerServiceTests
{
    [TestClass]
    public class FilterSeriesAsync_Should
    {
        [TestMethod]
        public async Task ThrowException_WhenPassedInvalidPageSize()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            string validFilter = "testPlayer";
            int invalidPageSize = -1;
            int validPageNumber = 1;

            PlayerService SUT = new PlayerService(
                    pandaScoreClientMock.Object,
                    dataContextMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(
                    () => SUT.FilterPlayersAsync(validFilter, validPageNumber, invalidPageSize));
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedInvalidPageNumber()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            string validFilter = "testPlayer";
            int validPageSize = 10;
            int invalidPageNumber = -1;

            PlayerService SUT = new PlayerService(
                    pandaScoreClientMock.Object,
                    dataContextMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(
                    () => SUT.FilterPlayersAsync(validFilter, invalidPageNumber, validPageSize));
        }
    }
}
