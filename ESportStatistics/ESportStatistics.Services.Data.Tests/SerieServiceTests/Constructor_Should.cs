﻿using Moq;
using System;
using ESportStatistics.Data.Context;
using ESportStatistics.Core.Services;
using ESportStatistics.Services.External;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESportStatistics.Services.Data.Tests.SerieServiceTests
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
                    () => new SerieService(
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
                () => new SerieService(
                    null,
                    dataContextMock.Object));
        }
    }
}
