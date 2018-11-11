using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Tests.Areas.Statistics.PlayerControllerTests
{
    [TestClass]
    public class IndexAction_Should
    {
        [TestMethod]
        public async Task ReturnViewResult_WhenCalled()
        {
            // Arrange
            Mock<IPlayerService> playerServiceMock = new Mock<IPlayerService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Player> players = new PagedList<Player>(new List<Player>().AsQueryable(), validPageNumber, validPageSize);

            playerServiceMock.Setup(mock => mock.FilterPlayersAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(players));

            PlayerController SUT = new PlayerController(
                playerServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock);

            // Act
            var result = await SUT.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel_WhenCalled()
        {
            // Arrange
            Mock<IPlayerService> playerServiceMock = new Mock<IPlayerService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Player> players = new PagedList<Player>(new List<Player>().AsQueryable(), validPageNumber, validPageSize);

            playerServiceMock.Setup(mock => mock.FilterPlayersAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(players));

            PlayerController SUT = new PlayerController(
                playerServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock);

            // Act
            var result = await SUT.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(PlayerIndexViewModel));
        }
    }
}
