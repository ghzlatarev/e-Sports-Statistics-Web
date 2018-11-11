using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Matches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Match = ESportStatistics.Data.Models.Match;

namespace ESportStatistics.Web.Tests.Areas.Statistics.MatchControllerTests
{
    [TestClass]
    public class DetailsAction_Should
    {
        [TestMethod]
        public async Task ReturnViewResult_WhenCalled()
        {
            // Arrange
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Match validMatchResult = new Match();

            matchServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validMatchResult));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
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
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Match validMatchResult = new Match();

            matchServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validMatchResult));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Details(validId) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(MatchDetailsViewModel));
        }

        [TestMethod]
        public async Task CallFindAsync_WhenCalled()
        {
            // Arrange
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Match validMatchResult = new Match();

            matchServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validMatchResult));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Details(validId);

            // Assert
            matchServiceMock.Verify(mock => mock.FindAsync(validId), Times.Once);
        }

        [TestMethod]
        public async Task ThrowApplicationException_WhenPassedNullId()
        {
            // Arrange
            Mock<IMatchService> matchServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Match validMatchResult = new Match();

            matchServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validMatchResult));

            MatchController SUT = new MatchController(
                matchServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Details(null));
        }

        [TestMethod]
        public async Task ThrowApplicationException_WhenPassedMatchIsNull()
        {
            // Arrange
            Mock<IMatchService> ietmServiceMock = new Mock<IMatchService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Match validMatchResult = null;

            ietmServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validMatchResult));

            MatchController SUT = new MatchController(
                ietmServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Details(validId));
        }
    }
}
