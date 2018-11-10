using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Services.External;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ESportStatistics.Services.Data.Tests.TournamentServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullDataContext()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClient = new Mock<IPandaScoreClient>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                    () => new TournamentService(
                        pandaScoreClient.Object,
                        null));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullPandaScoreClient()
        {
            // Arrange
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new TournamentService(
                    null,
                    dataContextMock.Object));
        }
    }
}
