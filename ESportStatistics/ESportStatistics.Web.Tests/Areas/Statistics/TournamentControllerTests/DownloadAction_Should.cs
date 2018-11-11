using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Champions;
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

namespace ESportStatistics.Web.Tests.Areas.Statistics.TournamentControllerTests
{
    [TestClass]
    public class DownloadAction_Should
    {
        [TestMethod]
        public async Task ReturnPartialViewResult_WhenCalled()
        {
            // Arrange
            Mock<ITournamentService> tournamentServiceMock = new Mock<ITournamentService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(TournamentDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "tournaments";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Tournament> tournaments = new PagedList<Tournament>(
                new List<Tournament>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<TournamentDownloadViewModel> tournamentDownloadViewModels = new PagedList<TournamentDownloadViewModel>(
                new List<TournamentDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            tournamentServiceMock.Setup(mock => mock.FilterTournamentsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(tournaments));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(tournamentDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            TournamentController SUT = new TournamentController(
                tournamentServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            Assert.IsInstanceOfType(result, typeof(FileResult));
        }

        [TestMethod]
        public async Task CallFilterTournamentsAsync_WhenCalled()
        {
            // Arrange
            Mock<ITournamentService> tournamentServiceMock = new Mock<ITournamentService>();
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

            IPagedList<Tournament> tournaments = new PagedList<Tournament>(
                new List<Tournament>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<TournamentDownloadViewModel> tournamentDownloadViewModels = new PagedList<TournamentDownloadViewModel>(
                new List<TournamentDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            tournamentServiceMock.Setup(mock => mock.FilterTournamentsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(tournaments));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(tournamentDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            TournamentController SUT = new TournamentController(
                tournamentServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            tournamentServiceMock.Verify(mock =>
                mock.FilterTournamentsAsync(validSortOrder, validFilter, validPageNumber, validPageSize),
                Times.Once);
        }

        [TestMethod]
        public async Task CallCreatePDF_WhenCalled()
        {
            // Arrange
            Mock<ITournamentService> tournamentServiceMock = new Mock<ITournamentService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(TournamentDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "tournaments";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Tournament> tournaments = new PagedList<Tournament>(
                new List<Tournament>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<TournamentDownloadViewModel> tournamentDownloadViewModels = new PagedList<TournamentDownloadViewModel>(
                new List<TournamentDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            tournamentServiceMock.Setup(mock => mock.FilterTournamentsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(tournaments));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(tournamentDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            TournamentController SUT = new TournamentController(
                tournamentServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.CreatePDF(tournamentDownloadViewModels, validFileParameters, validCollection),
                Times.Once);
        }

        [TestMethod]
        public async Task CallGetFileBytesAsync_WhenCalled()
        {
            // Arrange
            Mock<ITournamentService> tournamentServiceMock = new Mock<ITournamentService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(TournamentDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "tournaments";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Tournament> tournaments = new PagedList<Tournament>(
                new List<Tournament>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<TournamentDownloadViewModel> tournamentDownloadViewModels = new PagedList<TournamentDownloadViewModel>(
                new List<TournamentDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            tournamentServiceMock.Setup(mock => mock.FilterTournamentsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(tournaments));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(tournamentDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            TournamentController SUT = new TournamentController(
                tournamentServiceMock.Object,
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
            Mock<ITournamentService> tournamentServiceMock = new Mock<ITournamentService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(TournamentDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "tournaments";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Tournament> tournaments = new PagedList<Tournament>(
                new List<Tournament>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<TournamentDownloadViewModel> tournamentDownloadViewModels = new PagedList<TournamentDownloadViewModel>(
                new List<TournamentDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            tournamentServiceMock.Setup(mock => mock.FilterTournamentsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(tournaments));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(tournamentDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            TournamentController SUT = new TournamentController(
                tournamentServiceMock.Object,
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
        public async Task ThrowApplicationException_WhenTournamentsIsNull()
        {
            // Arrange
            Mock<ITournamentService> tournamentServiceMock = new Mock<ITournamentService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Tournament> tournaments = null;

            tournamentServiceMock.Setup(mock => mock.FilterTournamentsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(tournaments));

            TournamentController SUT = new TournamentController(
                tournamentServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize));
        }
    }
}
