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
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Tests.Areas.Statistics.TournamentControllerTests
{
    [TestClass]
    public class IndexAction_Should
    {
        [TestMethod]
        public async Task ReturnViewResult_WhenCalled()
        {
            // Arrange
            Mock<ITournamentService> tournamentServiceMock = new Mock<ITournamentService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Tournament> tournaments = new PagedList<Tournament>(new List<Tournament>().AsQueryable(), validPageNumber, validPageSize);

            tournamentServiceMock.Setup(mock => mock.FilterTournamentsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(tournaments));

            TournamentController SUT = new TournamentController(
                tournamentServiceMock.Object,
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
            Mock<ITournamentService> tournamentServiceMock = new Mock<ITournamentService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Tournament> tournaments = new PagedList<Tournament>(new List<Tournament>().AsQueryable(), validPageNumber, validPageSize);

            tournamentServiceMock.Setup(mock => mock.FilterTournamentsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(tournaments));

            TournamentController SUT = new TournamentController(
                tournamentServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock);

            // Act
            var result = await SUT.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(TournamentIndexViewModel));
        }
    }
}
