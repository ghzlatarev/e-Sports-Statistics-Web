using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Champions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Tests.Areas.Statistics.ChampionControllerTests
{
    [TestClass]
    public class DownloadAction_Should
    {
        [TestMethod]
        public async Task ReturnPartialViewResult_WhenCalled()
        {
            // Arrange
            Mock<IChampionService> championServiceMock = new Mock<IChampionService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(ChampionDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "champions";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Champion> champions = new PagedList<Champion>(
                new List<Champion>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<ChampionDownloadViewModel> championDownloadViewModels = new PagedList<ChampionDownloadViewModel>(
                new List<ChampionDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            championServiceMock.Setup(mock => mock.FilterChampionsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(champions));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(championDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            ChampionController SUT = new ChampionController(
                championServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            Assert.IsInstanceOfType(result, typeof(FileResult));
        }

        [TestMethod]
        public async Task CallFilterChampionsAsync_WhenCalled()
        {
            // Arrange
            Mock<IChampionService> championServiceMock = new Mock<IChampionService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(ChampionDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "champions";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Champion> champions = new PagedList<Champion>(
                new List<Champion>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<ChampionDownloadViewModel> championDownloadViewModels = new PagedList<ChampionDownloadViewModel>(
                new List<ChampionDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            championServiceMock.Setup(mock => mock.FilterChampionsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(champions));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(championDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            ChampionController SUT = new ChampionController(
                championServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            championServiceMock.Verify(mock =>
                mock.FilterChampionsAsync(validSortOrder, validFilter, validPageNumber, validPageSize),
                Times.Once);
        }

        [TestMethod]
        public async Task CallCreatePDF_WhenCalled()
        {
            // Arrange
            Mock<IChampionService> championServiceMock = new Mock<IChampionService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(ChampionDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "champions";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Champion> champions = new PagedList<Champion>(
                new List<Champion>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<ChampionDownloadViewModel> championDownloadViewModels = new PagedList<ChampionDownloadViewModel>(
                new List<ChampionDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            championServiceMock.Setup(mock => mock.FilterChampionsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(champions));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(championDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            ChampionController SUT = new ChampionController(
                championServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.CreatePDF(championDownloadViewModels, validFileParameters, validCollection),
                Times.Once);
        }

        [TestMethod]
        public async Task CallGetFileBytesAsync_WhenCalled()
        {
            // Arrange
            Mock<IChampionService> championServiceMock = new Mock<IChampionService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(ChampionDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "champions";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Champion> champions = new PagedList<Champion>(
                new List<Champion>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<ChampionDownloadViewModel> championDownloadViewModels = new PagedList<ChampionDownloadViewModel>(
                new List<ChampionDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            championServiceMock.Setup(mock => mock.FilterChampionsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(champions));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(championDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            ChampionController SUT = new ChampionController(
                championServiceMock.Object,
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
            Mock<IChampionService> championServiceMock = new Mock<IChampionService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(ChampionDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "champions";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Champion> champions = new PagedList<Champion>(
                new List<Champion>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<ChampionDownloadViewModel> championDownloadViewModels = new PagedList<ChampionDownloadViewModel>(
                new List<ChampionDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            championServiceMock.Setup(mock => mock.FilterChampionsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(champions));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(championDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            ChampionController SUT = new ChampionController(
                championServiceMock.Object,
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
        public async Task ThrowApplicationException_WhenChampionsIsNull()
        {
            // Arrange
            Mock<IChampionService> championServiceMock = new Mock<IChampionService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Champion> champions = null;

            championServiceMock.Setup(mock => mock.FilterChampionsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(champions));

            ChampionController SUT = new ChampionController(
                championServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize));
        }
    }
}
