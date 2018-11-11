using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Champions;
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

namespace ESportStatistics.Web.Tests.Areas.Statistics.ItemControllerTests
{
    [TestClass]
    public class IndexAction_Should
    {
        [TestMethod]
        public async Task ReturnViewResult_WhenCalled()
        {
            // Arrange
            Mock<IChampionService> championServiceMock = new Mock<IChampionService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());
            
            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;
            
            IPagedList<Champion> champions = new PagedList<Champion>(new List<Champion>().AsQueryable(), validPageNumber, validPageSize);

            championServiceMock.Setup(mock => mock.FilterChampionsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(champions));
            
            ChampionController SUT = new ChampionController(
                championServiceMock.Object,
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
            Mock<IChampionService> championServiceMock = new Mock<IChampionService>();
            Mock<IPDFService> pDFServiceMock = new Mock<IPDFService>();
            IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());

            string validSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            IPagedList<Champion> champions = new PagedList<Champion>(new List<Champion>().AsQueryable(), validPageNumber, validPageSize);

            championServiceMock.Setup(mock => mock.FilterChampionsAsync(validSortOrder, validFilter, validPageNumber, validPageSize))
                .Returns(Task.FromResult(champions));

            ChampionController SUT = new ChampionController(
                championServiceMock.Object,
                pDFServiceMock.Object,
                memoryCacheMock);

            // Act
            var result = await SUT.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(ChampionIndexViewModel));
        }
    }
}
