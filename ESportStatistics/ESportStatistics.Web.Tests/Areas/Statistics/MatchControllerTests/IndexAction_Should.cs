using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Matches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Match = ESportStatistics.Data.Models.Match;

namespace ESportStatistics.Web.Tests.Areas.Statistics.MatchControllerTests
{
    [TestClass]
    public class IndexAction_Should
    {
        [TestMethod]
        public async Task ReturnViewResult_WhenCalled()
        {
            // Arrange
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Match> matches = new PagedList<Match>(new List<Match>().AsQueryable(), validPageNumber, validPageSize);

            matchServiceMock.Setup(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(matches));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
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
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Match> matches = new PagedList<Match>(new List<Match>().AsQueryable(), validPageNumber, validPageSize);

            matchServiceMock.Setup(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(matches));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock);

            // Act
            var result = await SUT.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(MatchIndexViewModel));
        }
    }
}
