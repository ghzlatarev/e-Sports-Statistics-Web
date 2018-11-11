using ESportStatistics.Core.Services;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.External;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            string validSortOrder = "name_asc";
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

                result = await SUT.FilterSeriesAsync(validSortOrder, validFilter, validPageNumber, validPageSize);
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

        [TestMethod]
        public async Task FindAsync_ShouldReturnSerie_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FindAsync_ShouldReturnSerie_WhenPassedValidParameters")
                .Options;

            Guid validId = Guid.NewGuid();

            Serie validPlayer = new Serie
            {
                Id = validId,
                Name = "testChamp",
                Slug = "testSlug",
                Season = "testSeason"
            };

            Serie result = null;

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Series.AddAsync(validPlayer);
                await actContext.SaveChangesAsync();

                SerieService SUT = new SerieService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FindAsync(validId.ToString());
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Series.Any(c => c.Id.Equals(result.Id)));
                Assert.IsTrue(assertContext.Series.Any(c => c.Name.Equals(result.Name)));
                Assert.IsTrue(assertContext.Series.Any(c => c.Slug.Equals(result.Slug)));
                Assert.IsTrue(assertContext.Series.Any(c => c.Season.Equals(result.Season)));
            }
        }

        [TestMethod]
        public async Task RebaseSeriesAsync_ShouldRepopulateSpellSerie_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RebaseSeriesAsync_ShouldRepopulateSerieTable_WhenPassedValidParameters")
                .Options;

            string validAccessToken = string.Empty;
            string validCollectionName = "series";
            int validPageSize = 100;

            Serie validSerie = new Serie
            {
                Id = Guid.NewGuid(),
                Name = "testSerie",
                DeletedOn = DateTime.UtcNow.AddHours(2),
                IsDeleted = true
            };

            IEnumerable<Serie> validSerieList = new List<Serie>()
            {
                validSerie
            };

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                pandaScoreClientMock
                    .Setup(mock => mock.GetEntitiesParallel<Serie>(validAccessToken, validCollectionName, validPageSize))
                    .Returns(Task.FromResult(validSerieList));

                SerieService SUT = new SerieService(
                    pandaScoreClientMock.Object,
                    actContext);

                await SUT.RebaseSeriesAsync(validAccessToken);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Series.Count() == 1);
                Assert.IsTrue(assertContext.Series.Contains(validSerie));
            }
        }
    }
}
