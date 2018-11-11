using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Champions;
using ESportStatistics.Web.Areas.Statistics.Models.Spells;
using ESportStatistics.Web.Areas.Statistics.Models.Teams;
using ESportStatistics.Web.Areas.Statistics.Models.Tournaments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Tests.Areas.Statistics.SpellControllerTests
{
    [TestClass]
    public class DownloadAction_Should
    {
        [TestMethod]
        public async Task ReturnPartialViewResult_WhenCalled()
        {
            // Arrange
            Mock<ISpellService> spellServiceMock = new Mock<ISpellService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(SpellDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "spells";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Spell> spells = new PagedList<Spell>(
                new List<Spell>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<SpellDownloadViewModel> spellDownloadViewModels = new PagedList<SpellDownloadViewModel>(
                new List<SpellDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            spellServiceMock.Setup(mock => mock.FilterSpellsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(spells));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(spellDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            SpellController SUT = new SpellController(
                spellServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            var result = await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            Assert.IsInstanceOfType(result, typeof(FileResult));
        }

        [TestMethod]
        public async Task CallFilterSpellsAsync_WhenCalled()
        {
            // Arrange
            Mock<ISpellService> spellServiceMock = new Mock<ISpellService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(SpellDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "spells";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Spell> spells = new PagedList<Spell>(
                new List<Spell>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<SpellDownloadViewModel> spellDownloadViewModels = new PagedList<SpellDownloadViewModel>(
                new List<SpellDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            spellServiceMock.Setup(mock => mock.FilterSpellsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(spells));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(spellDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            SpellController SUT = new SpellController(
                spellServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            spellServiceMock.Verify(mock =>
                 mock.FilterSpellsAsync(validSortOrder, validFilter, validPageNumber, validPageSize),
                 Times.Once);
        }

        [TestMethod]
        public async Task CallCreatePDF_WhenCalled()
        {
            // Arrange
            Mock<ISpellService> spellServiceMock = new Mock<ISpellService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(SpellDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "spells";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Spell> spells = new PagedList<Spell>(
                new List<Spell>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<SpellDownloadViewModel> spellDownloadViewModels = new PagedList<SpellDownloadViewModel>(
                new List<SpellDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            spellServiceMock.Setup(mock => mock.FilterSpellsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(spells));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(spellDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            SpellController SUT = new SpellController(
                spellServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.CreatePDF(spellDownloadViewModels, validFileParameters, validCollection),
                Times.Once);
        }

        [TestMethod]
        public async Task CallGetFileBytesAsync_WhenCalled()
        {
            // Arrange
            Mock<ISpellService> spellServiceMock = new Mock<ISpellService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(SpellDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "spells";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Spell> spells = new PagedList<Spell>(
                new List<Spell>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<SpellDownloadViewModel> spellDownloadViewModels = new PagedList<SpellDownloadViewModel>(
                new List<SpellDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            spellServiceMock.Setup(mock => mock.FilterSpellsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(spells));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(spellDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            SpellController SUT = new SpellController(
                spellServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.GetFileBytesAsync(validFileName),
                Times.Once);
        }

        [TestMethod]
        public async Task CallDeleteFile_WhenCalled()
        {
            // Arrange
            Mock<ISpellService> spellServiceMock = new Mock<ISpellService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            IList<string> validFileParameters = typeof(SpellDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            string validCollection = "spells";

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            string validFileName = string.Empty;
            byte[] validFileBytes = new byte[0];

            IPagedList<Spell> spells = new PagedList<Spell>(
                new List<Spell>().AsQueryable(),
                validPageNumber,
                validPageSize);

            IPagedList<SpellDownloadViewModel> spellDownloadViewModels = new PagedList<SpellDownloadViewModel>(
                new List<SpellDownloadViewModel>().AsQueryable(),
                validPageNumber,
                validPageSize);

            spellServiceMock.Setup(mock => mock.FilterSpellsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(spells));

            pDFServiceMock
                .Setup(mock => mock.CreatePDF(spellDownloadViewModels, validFileParameters, validCollection))
                .Returns(validFileName);

            pDFServiceMock
                .Setup(mock => mock.GetFileBytesAsync(validFileName))
                .Returns(Task.FromResult(validFileBytes));

            pDFServiceMock
                .Setup(mock => mock.DeleteFile(validFileName));

            SpellController SUT = new SpellController(
                spellServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act
            await SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize);

            // Assert
            pDFServiceMock.Verify(mock =>
                mock.DeleteFile(validFileName),
                Times.Once);
        }

        [TestMethod]
        public async Task ThrowApplicationException_WhenSpellsIsNull()
        {
            // Arrange
            Mock<ISpellService> spellServiceMock = new Mock<ISpellService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Spell> spells = null;

            spellServiceMock.Setup(mock => mock.FilterSpellsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(spells));

            SpellController SUT = new SpellController(
                spellServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
                SUT.Download(validSortOrder, validFilter, validPageNumber, validPageSize));
        }
    }
}
