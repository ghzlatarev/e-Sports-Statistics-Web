using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Leagues;
using ESportStatistics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Tests.Areas.Statistics.LeagueControllerTests
{
    [TestClass]
    public class FilterAction_Should
    {
        [TestMethod]
        public async Task ReturnPartialViewResult_WhenCalled()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<League> leagues = new PagedList<League>(new List<League>().AsQueryable(), validPageNumber, validPageSize);

            leagueServiceMock.Setup(mock => mock.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(leagues));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Filter(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel_WhenCalled()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<League> leagues = new PagedList<League>(new List<League>().AsQueryable(), validPageNumber, validPageSize);

            leagueServiceMock.Setup(mock => mock.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(leagues));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Filter(validSortOrder, validFilter, validPageSize, validPageNumber) as PartialViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(TableViewModel<LeagueViewModel>));
        }

        [TestMethod]
        public async Task CallFilterLeaguesAsync_WhenCalled()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<League> leagues = new PagedList<League>(new List<League>().AsQueryable(), validPageNumber, validPageSize);

            leagueServiceMock.Setup(mock => mock.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(leagues));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Filter(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            leagueServiceMock
                .Verify(mock => mock.FilterLeaguesAsync(validSortOrder, validFilter, validPageNumber, validPageSize),
                Times.Once);
        }
    }
}
