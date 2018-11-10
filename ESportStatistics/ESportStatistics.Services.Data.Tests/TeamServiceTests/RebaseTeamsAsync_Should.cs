using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.External;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Tests.TeamServiceTests
{
    [TestClass]
    public class RebaseTeamsAsync_Should
    {
        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedNullAccessToken()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            // Act
            TeamService SUT = new TeamService(
                   pandaScoreClientMock.Object,
                   dataContextMock.Object);

            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
               await SUT.RebaseTeamsAsync(null));
        }
    }
}
