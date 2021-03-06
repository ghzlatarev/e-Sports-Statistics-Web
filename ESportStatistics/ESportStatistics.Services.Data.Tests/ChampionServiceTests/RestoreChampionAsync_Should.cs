﻿using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Services.External;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Data.Tests.ChampionServiceTests
{
    [TestClass]
    public class RestoreChampionAsync_Should
    {
        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedNullId()
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            // Act
            ChampionService SUT = new ChampionService(
                   pandaScoreClientMock.Object,
                   dataContextMock.Object);

            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
               await SUT.RebaseChampionsAsync(null));
        }

        [DataTestMethod]
        [DataRow("invalidId")]
        [DataRow("123231-321321-ewqewq")]
        public async Task ThrowArgumentException_WhenPassedInvalidId(string invalidId)
        {
            // Arrange
            Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();
            Mock<DataContext> dataContextMock = new Mock<DataContext>();

            // Act
            ChampionService SUT = new ChampionService(
                   pandaScoreClientMock.Object,
                   dataContextMock.Object);

            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
               await SUT.DeleteChampionAsync(invalidId));
        }
    }
}
