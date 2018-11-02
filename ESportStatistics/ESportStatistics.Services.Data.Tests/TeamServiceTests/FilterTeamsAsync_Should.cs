﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Services.External;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ESportStatistics.Services.Data.Tests.TeamServiceTests
{
    [TestClass]
    public class FilterTeamsAsync_Should
    {
        [TestMethod]
        public async Task ThrowException_WhenPassedInvalidPageSize()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            string validFilter = "testTeam";
            int invalidPageSize = -1;
            int validPageNumber = 1;

            TeamService SUT = new TeamService(
                    pandaScoreClientMock.Object,
                    dataContextMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(
                    () => SUT.FilterTeamsAsync(validFilter, validPageNumber, invalidPageSize));
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedInvalidPageNumber()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            string validFilter = "testTeam";
            int validPageSize = 10;
            int invalidPageNumber = -1;

            TeamService SUT = new TeamService(
                    pandaScoreClientMock.Object,
                    dataContextMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(
                    () => SUT.FilterTeamsAsync(validFilter, invalidPageNumber, validPageSize));
        }
    }
}
