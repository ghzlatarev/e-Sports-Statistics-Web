using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Tests.Areas.Statistics.PlayerControllerTests
{
    [TestClass]
    public class DownloadAction_Should
    {
        [TestMethod]
        public async Task ReturnPartialViewResult_WhenCalled()
        {
            // Arrange
            Mock<IPlayerService> playerServiceMock = new Mock<IPlayerService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(PlayerDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "players";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Player> players = new PagedList<Player>(
                new List<Player>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<PlayerDownloadViewModel> playerDownloadViewModels = new PagedList<PlayerDownloadViewModel>(
                new List<PlayerDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            playerServiceMock.Setup(mock => mock.FilterPlayersAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(players));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(playerDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            PlayerController SUT = new PlayerController(
                playerServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            Assert.IsInstanceOfType(result, typeof(FileResult));
        }

        [TestMethod]
        public async Task CallFilterPlayersAsync_WhenCalled()
        {
            // Arrange
            Mock<IPlayerService> playerServiceMock = new Mock<IPlayerService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(PlayerDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "players";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Player> players = new PagedList<Player>(
                new List<Player>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<PlayerDownloadViewModel> playerDownloadViewModels = new PagedList<PlayerDownloadViewModel>(
                new List<PlayerDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            playerServiceMock.Setup(mock => mock.FilterPlayersAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(players));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(playerDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            PlayerController SUT = new PlayerController(
                playerServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            playerServiceMock.Verify(mock =>
                mock.FilterPlayersAsync(validSortOrder, validFilter, validPageNumber, validPageSize),
                Times.Once);
        }

        [TestMethod]
        public async Task CallCreatePDF_WhenCalled()
        {
            // Arrange
            Mock<IPlayerService> playerServiceMock = new Mock<IPlayerService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(PlayerDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "players";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Player> players = new PagedList<Player>(
                new List<Player>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<PlayerDownloadViewModel> playerDownloadViewModels = new PagedList<PlayerDownloadViewModel>(
                new List<PlayerDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            playerServiceMock.Setup(mock => mock.FilterPlayersAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(players));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(playerDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            PlayerController SUT = new PlayerController(
                playerServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.CreatePDF(playerDownloadViewModels, validFileParameters, validCollection),
                Times.Once);
        }

        [TestMethod]
        public async Task CallGetFileBytesAsync_WhenCalled()
        {
            // Arrange
            Mock<IPlayerService> playerServiceMock = new Mock<IPlayerService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(PlayerDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "players";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Player> players = new PagedList<Player>(
                new List<Player>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<PlayerDownloadViewModel> playerDownloadViewModels = new PagedList<PlayerDownloadViewModel>(
                new List<PlayerDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            playerServiceMock.Setup(mock => mock.FilterPlayersAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(players));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(playerDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            PlayerController SUT = new PlayerController(
                playerServiceMock.Object,
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
            Mock<IPlayerService> playerServiceMock = new Mock<IPlayerService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(PlayerDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "players";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Player> players = new PagedList<Player>(
                new List<Player>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<PlayerDownloadViewModel> playerDownloadViewModels = new PagedList<PlayerDownloadViewModel>(
                new List<PlayerDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            playerServiceMock.Setup(mock => mock.FilterPlayersAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(players));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(playerDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            PlayerController SUT = new PlayerController(
                playerServiceMock.Object,
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
        public async Task ThrowApplicationException_WhenPlayersIsNull()
        {
            // Arrange
            Mock<IPlayerService> playerServiceMock = new Mock<IPlayerService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Player> players = null;

            playerServiceMock.Setup(mock => mock.FilterPlayersAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(players));

            PlayerController SUT = new PlayerController(
                playerServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize));
        }
    }
}
