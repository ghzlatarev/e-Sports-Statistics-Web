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
using System.Threading.Tasks;

namespace ESportStatistics.Web.Tests.Areas.Statistics.LeagueControllerTests
{
    [TestClass]
    public class DetailsAction_Should
    {
        [TestMethod]
        public async Task ReturnViewResult_WhenCalled()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            League validLeagueResult = new League();

            leagueServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validLeagueResult));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Details(validId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel_WhenCalled()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            League validLeagueResult = new League();

            leagueServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validLeagueResult));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Details(validId) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(LeagueDetailsViewModel));
        }

        [TestMethod]
        public async Task CallFindAsync_WhenCalled()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            League validLeagueResult = new League();

            leagueServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validLeagueResult));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Details(validId);

            // Assert
            leagueServiceMock.Verify(mock => mock.FindAsync(validId), Times.Once);
        }

        [TestMethod]
        public async Task ThrowApplicationException_WhenPassedNullId()
        {
            // Arrange
            Mock<ILeagueService> leagueServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            League validLeagueResult = new League();

            leagueServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validLeagueResult));

            LeagueController SUT = new LeagueController(
                leagueServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Details(null));
        }

        [TestMethod]
        public async Task ThrowApplicationException_WhenPassedLeagueIsNull()
        {
            // Arrange
            Mock<ILeagueService> ietmServiceMock = new Mock<ILeagueService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            League validLeagueResult = null;

            ietmServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validLeagueResult));

            LeagueController SUT = new LeagueController(
                ietmServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Details(validId));
        }
    }
}
