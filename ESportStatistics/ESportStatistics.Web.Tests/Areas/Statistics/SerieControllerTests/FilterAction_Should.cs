using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Series;
using ESportStatistics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Tests.Areas.Statistics.SerieControllerTests
{
    [TestClass]
    public class FilterAction_Should
    {
        [TestMethod]
        public async Task ReturnPartialViewResult_WhenCalled()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Serie> teams = new PagedList<Serie>(new List<Serie>().AsQueryable(), validPageNumber, validPageSize);

            serieServiceMock.Setup(mock => mock.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(teams));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Filter(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel_WhenCalled()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Serie> teams = new PagedList<Serie>(new List<Serie>().AsQueryable(), validPageNumber, validPageSize);

            serieServiceMock.Setup(mock => mock.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(teams));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Filter(validSortOrder, validFilter, validPageNumber, validPageSize) as PartialViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(TableViewModel<SerieViewModel>));
        }

        [TestMethod]
        public async Task CallFilterSeriesAsync_WhenCalled()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Serie> teams = new PagedList<Serie>(new List<Serie>().AsQueryable(), validPageNumber, validPageSize);

            serieServiceMock.Setup(mock => mock.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(teams));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Filter(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            serieServiceMock
                .Verify(mock => mock.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize),
                Times.Once);
        }
    }
}
