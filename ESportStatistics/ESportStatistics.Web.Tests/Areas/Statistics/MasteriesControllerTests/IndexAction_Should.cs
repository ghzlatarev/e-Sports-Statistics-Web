using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Masteries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Tests.Areas.Statistics.MasteryControllerTests
{
    [TestClass]
    public class IndexAction_Should
    {
        [TestMethod]
        public async Task ReturnViewResult_WhenCalled()
        {
            // Arrange
            Mock<IMasteryService> masteryServiceMock = new Mock<IMasteryService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Mastery> masterys = new PagedList<Mastery>(new List<Mastery>().AsQueryable(), validPageNumber, validPageSize);

            masteryServiceMock.Setup(mock => mock.FilterMasteriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(masterys));

            MasteryController SUT = new MasteryController(
                masteryServiceMock.Object,
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
            Mock<IMasteryService> masteryServiceMock = new Mock<IMasteryService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Mastery> masterys = new PagedList<Mastery>(new List<Mastery>().AsQueryable(), validPageNumber, validPageSize);

            masteryServiceMock.Setup(mock => mock.FilterMasteriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(masterys));

            MasteryController SUT = new MasteryController(
                masteryServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock);

            // Act
            var result = await SUT.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(MasteryIndexViewModel));
        }
    }
}
