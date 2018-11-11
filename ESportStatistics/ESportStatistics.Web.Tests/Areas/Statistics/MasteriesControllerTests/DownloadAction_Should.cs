using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Leagues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Tests.Areas.Statistics.MasteryControllerTests
{
    [TestClass]
    public class DownloadAction_Should
    {
        [TestMethod]
        public async Task ReturnPartialViewResult_WhenCalled()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(LeagueDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "leagues";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<League> leagues = new PagedList<League>(
                new List<League>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<LeagueDownloadViewModel> leagueDownloadViewModels = new PagedList<LeagueDownloadViewModel>(
                new List<LeagueDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            leagueServiceMock.Setup(mock => mock.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(leagues));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(leagueDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            Assert.IsInstanceOfType(result, typeof(FileResult));
        }

        [TestMethod]
        public async Task CallFilterLeaguesAsync_WhenCalled()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(LeagueDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "leagues";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<League> leagues = new PagedList<League>(
                new List<League>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<LeagueDownloadViewModel> leagueDownloadViewModels = new PagedList<LeagueDownloadViewModel>(
                new List<LeagueDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            leagueServiceMock.Setup(mock => mock.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(leagues));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(leagueDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            leagueServiceMock.Verify(mock =>
                mock.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize),
                Times.Once);
        }

        [TestMethod]
        public async Task CallCreatePDF_WhenCalled()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(LeagueDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "leagues";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<League> leagues = new PagedList<League>(
                new List<League>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<LeagueDownloadViewModel> leagueDownloadViewModels = new PagedList<LeagueDownloadViewModel>(
                new List<LeagueDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            leagueServiceMock.Setup(mock => mock.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(leagues));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(leagueDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.CreatePDF(leagueDownloadViewModels, validFileParameters, validCollection),
                Times.Once);
        }

        [TestMethod]
        public async Task CallGetFileBytesAsync_WhenCalled()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(LeagueDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "leagues";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<League> leagues = new PagedList<League>(
                new List<League>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<LeagueDownloadViewModel> leagueDownloadViewModels = new PagedList<LeagueDownloadViewModel>(
                new List<LeagueDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            leagueServiceMock.Setup(mock => mock.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(leagues));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(leagueDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.GetFileBytesAsync(validFileName),
                Times.Once);
        }

        [TestMethod]
        public async Task CallDeleteFile_WhenCalled()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(LeagueDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "leagues";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<League> leagues = new PagedList<League>(
                new List<League>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<LeagueDownloadViewModel> leagueDownloadViewModels = new PagedList<LeagueDownloadViewModel>(
                new List<LeagueDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            leagueServiceMock.Setup(mock => mock.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(leagues));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(leagueDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.DeleteFile(validFileName),
                Times.Once);
        }

        [TestMethod]
        public async Task ThrowApplicationException_WhenLeaguesIsNull()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<League> leagues = null;

            leagueServiceMock.Setup(mock => mock.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(leagues));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize));
        }
    }
}
