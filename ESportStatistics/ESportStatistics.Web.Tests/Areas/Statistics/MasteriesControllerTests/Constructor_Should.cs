using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ESportStatistics.Web.Tests.Areas.Statistics.MasteryControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WHenPassedNullMasteryService()
        {
            // Arrange
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                 new MasteryController(
                     null,
                     pDFServiceMock.Object,
                     memoryCacheMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WHenPassedNullPDFService()
        {
            // Arrange
            Mock<IMasteryService> masteryServiceMock = new Mock<IMasteryService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                 new MasteryController(
                     masteryServiceMock.Object,
                     null,
                     memoryCacheMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WHenPassedNullMemoryCache()
        {
            // Arrange
            Mock<IMasteryService> masteryServiceMock = new Mock<IMasteryService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                 new MasteryController(
                     masteryServiceMock.Object,
                     pDFServiceMock.Object,
                     null));
        }
    }
}
