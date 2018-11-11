using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Series;
using ESportStatistics.Web.Areas.Statistics.Models.Teams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Tests.Areas.Statistics.SerieControllerTests
{
    [TestClass]
    public class DetailsAction_Should
    {
        [TestMethod]
        public async Task ReturnViewResult_WhenCalled()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Serie validSerieResult = new Serie();

            serieServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validSerieResult));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Details(validId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel_WhenCalled()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Serie validSerieResult = new Serie();

            serieServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validSerieResult));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Details(validId) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(SerieDetailsViewModel));
        }

        [TestMethod]
        public async Task CallFindAsync_WhenCalled()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Serie validSerieResult = new Serie();

            serieServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validSerieResult));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Details(validId);

            // Assert
            serieServiceMock.Verify(mock => mock.FindAsync(validId), Times.Once);
        }

        [TestMethod]
        public async Task ThrowApplicationException_WhenPassedNullId()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string invalidId = null;

            Serie validSerieResult = new Serie();

            serieServiceMock.Setup(mock => mock.FindAsync(invalidId))
                .Returns(Task.FromResult(validSerieResult));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Details(invalidId));
        }

        [TestMethod]
        public async Task ThrowApplicationException_WhenPassedSerieIsNull()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Serie invalidSerieResult = null;

            serieServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(invalidSerieResult));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Details(validId));
        }
    }
}
