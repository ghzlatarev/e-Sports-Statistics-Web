using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ESportStatistics.Web.Tests.Areas.Statistics.SpellControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WHenPassedNullSpellService()
        {
            // Arrange
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();


            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                 new SpellController(
                     null,
                     pDFServiceMock.Object,
                     memoryCacheMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WHenPassedNullPDFService()
        {
            // Arrange
            Mock<ISpellService> spellServiceMock = new Mock<ISpellService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();


            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                 new SpellController(
                     spellServiceMock.Object,
                     null,
                     memoryCacheMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WHenPassedNullMemoryCache()
        {
            // Arrange
            Mock<ISpellService> spellServiceMock = new Mock<ISpellService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();


            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                 new SpellController(
                     spellServiceMock.Object,
                     pDFServiceMock.Object,
                     null));
        }
    }
}
