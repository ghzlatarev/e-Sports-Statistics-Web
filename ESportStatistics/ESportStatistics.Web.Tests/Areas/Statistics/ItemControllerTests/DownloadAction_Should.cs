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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Tests.Areas.Statistics.ItemControllerTests
{
    [TestClass]
    public class DownloadAction_Should
    {
        [TestMethod]
        public async Task ReturnPartialViewResult_WhenCalled()
        {
            // Arrange
            Mock<IItemService> itemServiceMock = new Mock<IItemService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(ItemDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "items";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Item> items = new PagedList<Item>(
                new List<Item>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<ItemDownloadViewModel> itemDownloadViewModels = new PagedList<ItemDownloadViewModel>(
                new List<ItemDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            itemServiceMock.Setup(mock => mock.FilterItemsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(items));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(itemDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            ItemController SUT = new ItemController(
                itemServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            Assert.IsInstanceOfType(result, typeof(FileResult));
        }

        [TestMethod]
        public async Task CallFilterItemsAsync_WhenCalled()
        {
            // Arrange
            Mock<IItemService> itemServiceMock = new Mock<IItemService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(ItemDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "items";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Item> items = new PagedList<Item>(
                new List<Item>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<ItemDownloadViewModel> itemDownloadViewModels = new PagedList<ItemDownloadViewModel>(
                new List<ItemDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            itemServiceMock.Setup(mock => mock.FilterItemsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(items));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(itemDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            ItemController SUT = new ItemController(
                itemServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            itemServiceMock.Verify(mock =>
                mock.FilterItemsAsync(validSortOrder, validFilter, validPageNumber, validPageSize),
                Times.Once);
        }

        [TestMethod]
        public async Task CallCreatePDF_WhenCalled()
        {
            // Arrange
            Mock<IItemService> itemServiceMock = new Mock<IItemService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(ItemDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "items";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Item> items = new PagedList<Item>(
                new List<Item>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<ItemDownloadViewModel> itemDownloadViewModels = new PagedList<ItemDownloadViewModel>(
                new List<ItemDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            itemServiceMock.Setup(mock => mock.FilterItemsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(items));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(itemDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            ItemController SUT = new ItemController(
                itemServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageSize, validPageNumber);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.CreatePDF(itemDownloadViewModels, validFileParameters, validCollection),
                Times.Once);
        }

        [TestMethod]
        public async Task CallGetFileBytesAsync_WhenCalled()
        {
            // Arrange
            Mock<IItemService> itemServiceMock = new Mock<IItemService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(ItemDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "items";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Item> items = new PagedList<Item>(
                new List<Item>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<ItemDownloadViewModel> itemDownloadViewModels = new PagedList<ItemDownloadViewModel>(
                new List<ItemDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            itemServiceMock.Setup(mock => mock.FilterItemsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(items));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(itemDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            ItemController SUT = new ItemController(
                itemServiceMock.Object,
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
            Mock<IItemService> itemServiceMock = new Mock<IItemService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(ItemDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "items";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Item> items = new PagedList<Item>(
                new List<Item>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<ItemDownloadViewModel> itemDownloadViewModels = new PagedList<ItemDownloadViewModel>(
                new List<ItemDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            itemServiceMock.Setup(mock => mock.FilterItemsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(items));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(itemDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            ItemController SUT = new ItemController(
                itemServiceMock.Object,
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
        public async Task ThrowApplicationException_WhenItemsIsNull()
        {
            // Arrange
            Mock<IItemService> itemServiceMock = new Mock<IItemService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Item> items = null;

            itemServiceMock.Setup(mock => mock.FilterItemsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(items));

            ItemController SUT = new ItemController(
                itemServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize));
        }
    }
}
