using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Matches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Match = ESportStatistics.Data.Models.Match;

namespace ESportStatistics.Web.Tests.Areas.Statistics.MatchControllerTests
{
    [TestClass]
    public class DownloadAction_Should
    {
        [TestMethod]
        public async Task ReturnPartialViewResult_WhenCalled()
        {
            // Arrange
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(MatchDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "matches";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Match> matches = new PagedList<Match>(
                new List<Match>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<MatchDownloadViewModel> matchDownloadViewModels = new PagedList<MatchDownloadViewModel>(
                new List<MatchDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            matchServiceMock.Setup(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(matches));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(matchDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            Assert.IsInstanceOfType(result, typeof(FileResult));
        }

        [TestMethod]
        public async Task CallFilterMatchesAsync_WhenCalled()
        {
            // Arrange
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(MatchDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "matches";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Match> matches = new PagedList<Match>(
                new List<Match>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<MatchDownloadViewModel> matchDownloadViewModels = new PagedList<MatchDownloadViewModel>(
                new List<MatchDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            matchServiceMock.Setup(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(matches));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(matchDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            matchServiceMock.Verify(mock =>
                mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize),
                Times.Once);
        }

        [TestMethod]
        public async Task CallCreatePDF_WhenCalled()
        {
            // Arrange
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(MatchDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "matches";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Match> matches = new PagedList<Match>(
                new List<Match>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<MatchDownloadViewModel> matchDownloadViewModels = new PagedList<MatchDownloadViewModel>(
                new List<MatchDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            matchServiceMock.Setup(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(matches));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(matchDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.CreatePDF(matchDownloadViewModels, validFileParameters, validCollection),
                Times.Once);
        }

        [TestMethod]
        public async Task CallGetFileBytesAsync_WhenCalled()
        {
            // Arrange
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(MatchDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "matches";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Match> matches = new PagedList<Match>(
                new List<Match>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<MatchDownloadViewModel> matchDownloadViewModels = new PagedList<MatchDownloadViewModel>(
                new List<MatchDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            matchServiceMock.Setup(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(matches));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(matchDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
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
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(MatchDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "matches";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Match> matches = new PagedList<Match>(
                new List<Match>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<MatchDownloadViewModel> matchDownloadViewModels = new PagedList<MatchDownloadViewModel>(
                new List<MatchDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            matchServiceMock.Setup(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(matches));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(matchDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
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
        public async Task ThrowApplicationException_WhenMatchesIsNull()
        {
            // Arrange
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Match> matches = null;

            matchServiceMock.Setup(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(matches));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize));
        }
    }
}
