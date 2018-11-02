using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.External;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ESportStatistics.Services.Data.Tests.SerieServiceTests
{
    [TestClass]
    public class WithInMemory
    {
        [TestMethod]
        public async Task FilterSeriesAsync_ShouldReturnSeries_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FilterSeriesAsync_ShouldReturnSeries_WhenPassedValidParameters")
                .Options;

            string validFilter = "testSerie";
            int validPageSize = 10;
            int validPageNumber = 1;
            string season = "2050";
            string validName = "testSerie";

            Serie validSerie = new Serie
            {
                Name = validName,
                Season = season
            };

            IEnumerable<Serie> result = new List<Serie>();

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Series.AddAsync(validSerie);
                await actContext.SaveChangesAsync();

                SerieService SUT = new SerieService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FilterSeriesAsync(validFilter, validPageNumber, validPageSize);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                var serie = result.ToArray()[0];

                Assert.IsTrue(assertContext.Series.Count().Equals(result.Count()));
                Assert.IsTrue(assertContext.Series.Any(s => s.Name.Equals(serie.Name)));
                Assert.IsTrue(assertContext.Series.Any(s => s.Season.Equals(serie.Season)));
            }
        }
    }
}
