using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Items;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Tests.Areas.Statistics.ItemControllerTests
{
    [TestClass]
    public class DetailsAction_Should
    {
        [TestMethod]
        public async Task ReturnViewResult_WhenCalled()
        {
            // Arrange
            Mock<IItemService> itemServiceMock = new Mock<IItemService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Item validItemResult = new Item();

            itemServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validItemResult));

            ItemController SUT = new ItemController(
                itemServiceMock.Object,
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
            Mock<IItemService> itemServiceMock = new Mock<IItemService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Item validItemResult = new Item();

            itemServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validItemResult));

            ItemController SUT = new ItemController(
                itemServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Details(validId) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(ItemDetailsViewModel));
        }

        [TestMethod]
        public async Task CallFindAsync_WhenCalled()
        {
            // Arrange
            Mock<IItemService> itemServiceMock = new Mock<IItemService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Item validItemResult = new Item();

            itemServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validItemResult));

            ItemController SUT = new ItemController(
                itemServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Details(validId);

            // Assert
            itemServiceMock.Verify(mock => mock.FindAsync(validId), Times.Once);
        }

        [TestMethod]
        public async Task ThrowApplicationException_WhenPassedNullId()
        {
            // Arrange
            Mock<IItemService> itemServiceMock = new Mock<IItemService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Item validItemResult = new Item();

            itemServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validItemResult));

            ItemController SUT = new ItemController(
                itemServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Details(null));
        }

        [TestMethod]
        public async Task ThrowApplicationException_WhenPassedItemIsNull()
        {
            // Arrange
            Mock<IItemService> itemServiceMock = new Mock<IItemService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validId = string.Empty;

            Item validItemResult = null;

            itemServiceMock.Setup(mock => mock.FindAsync(validId))
                .Returns(Task.FromResult(validItemResult));

            ItemController SUT = new ItemController(
                itemServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Details(validId));
        }
    }
}
