﻿using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Services.External;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Tests.MatchServiceTests
{
    [TestClass]
    public class RebaseMatchesAsync_Should
    {
        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedNullAccessToken()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            // Act
            MatchService SUT = new MatchService(
                   pandaScoreClientMock.Object,
                   dataContextMock.Object);

            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
               await SUT.RebaseMatchesAsync(null));
        }
    }
}
