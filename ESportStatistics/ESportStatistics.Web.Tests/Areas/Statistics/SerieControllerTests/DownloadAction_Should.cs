using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Champions;
using ESportStatistics.Web.Areas.Statistics.Models.Series;
using ESportStatistics.Web.Areas.Statistics.Models.Teams;
using ESportStatistics.Web.Areas.Statistics.Models.Tournaments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Tests.Areas.Statistics.SerieControllerTests
{
    [TestClass]
    public class DownloadAction_Should
    {
        [TestMethod]
        public async Task ReturnPartialViewResult_WhenCalled()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(SerieDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "series";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Serie> series = new PagedList<Serie>(
                new List<Serie>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<SerieDownloadViewModel> serieDownloadViewModels = new PagedList<SerieDownloadViewModel>(
                new List<SerieDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            serieServiceMock.Setup(mock => mock.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(series));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(serieDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            Assert.IsInstanceOfType(result, typeof(FileResult));
        }

        [TestMethod]
        public async Task CallFilterSeriesAsync_WhenCalled()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(SerieDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "series";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Serie> series = new PagedList<Serie>(
                new List<Serie>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<SerieDownloadViewModel> serieDownloadViewModels = new PagedList<SerieDownloadViewModel>(
                new List<SerieDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            serieServiceMock.Setup(mock => mock.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(series));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(serieDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            serieServiceMock.Verify(mock =>
                 mock.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize),
                 Times.Once);
        }

        [TestMethod]
        public async Task CallCreatePDF_WhenCalled()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(SerieDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "series";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Serie> series = new PagedList<Serie>(
                new List<Serie>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<SerieDownloadViewModel> serieDownloadViewModels = new PagedList<SerieDownloadViewModel>(
                new List<SerieDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            serieServiceMock.Setup(mock => mock.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(series));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(serieDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.CreatePDF(serieDownloadViewModels, validFileParameters, validCollection),
                Times.Once);
        }

        [TestMethod]
        public async Task CallGetFileBytesAsync_WhenCalled()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(SerieDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "series";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Serie> series = new PagedList<Serie>(
                new List<Serie>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<SerieDownloadViewModel> serieDownloadViewModels = new PagedList<SerieDownloadViewModel>(
                new List<SerieDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            serieServiceMock.Setup(mock => mock.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(series));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(serieDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.GetFileBytesAsync(validFileName),
                Times.Once);
        }

        [TestMethod]
        public async Task CallDeleteFile_WhenCalled()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(SerieDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "series";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Serie> series = new PagedList<Serie>(
                new List<Serie>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<SerieDownloadViewModel> serieDownloadViewModels = new PagedList<SerieDownloadViewModel>(
                new List<SerieDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            serieServiceMock.Setup(mock => mock.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(series));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(serieDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.DeleteFile(validFileName),
                Times.Once);
        }

        [TestMethod]
        public async Task ThrowApplicationException_WhenSeriesIsNull()
        {
            // Arrange
            Mock<ISerieService> serieServiceMock = new Mock<ISerieService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Serie> series = null;

            serieServiceMock.Setup(mock => mock.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(series));

            SerieController SUT = new SerieController(
                serieServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize));
        }
    }
}
