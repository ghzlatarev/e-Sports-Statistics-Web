using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Matches;
using ESportStatistics.Web.Models;
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
    public class FilterAction_Should
    {
        [TestMethod]
        public async Task ReturnPartialViewResult_WhenCalled()
        {
            // Arrange
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<ESportStatistics.Data.Models.Match> matches = new PagedList<ESportStatistics.Data.Models.Match>(new List<Match>().AsQueryable(), validPageNumber, validPageSize);

            matchServiceMock.Setup(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(matches));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
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
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<ESportStatistics.Data.Models.Match> matches = new PagedList<Match>(new List<Match>().AsQueryable(), validPageNumber, validPageSize);

            matchServiceMock.Setup(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(matches));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Filter(validSortOrder, validFilter, validPageSize, validPageNumber) as PartialViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(TableViewModel<MatchViewModel>));
        }

        [TestMethod]
        public async Task CallFilterMatchesAsync_WhenCalled()
        {
            // Arrange
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<ESportStatistics.Data.Models.Match> matches = new PagedList<ESportStatistics.Data.Models.Match>(new List<Match>().AsQueryable(), validPageNumber, validPageSize);

            matchServiceMock.Setup(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(matches));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Filter(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            matchServiceMock
                .Verify(mock => mock.FilterMatchesAsync(validSortOrder, validFilter, validPageNumber, validPageSize),
                Times.Once);
        }
    }
}
