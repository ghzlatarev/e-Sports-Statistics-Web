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

namespace ESportStatistics.Services.Data.Tests.SpellServiceTests
{
    [TestClass]
    public class WithInMemory
    {
        [TestMethod]
        public async Task FilterSpellsAsync_ShouldReturnSpells_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FilterSpellsAsync_ShouldReturnSpells_WhenPassedValidParameters")
                .Options;

            string validSortOrder = "name_asc";
            string validFilter = "testSpell";
            int validPageSize = 10;
            int validPageNumber = 1;

            string validName = "testSpell";

            Spell validSpell = new Spell
            {
                Id = Guid.NewGuid(),
                Name = validName
            };

            IEnumerable<Spell> result = new List<Spell>();

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Spells.AddAsync(validSpell);
                await actContext.SaveChangesAsync();

                SpellService SUT = new SpellService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FilterSpellsAsync(validSortOrder, validFilter, validPageNumber, validPageSize);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                var spell = result.ToArray()[0];

                Assert.IsTrue(assertContext.Spells.Count().Equals(result.Count()));
                Assert.IsTrue(assertContext.Spells.Any(s => s.Name.Equals(spell.Name)));
            }
        }

        [TestMethod]
        public async Task FindAsync_ShouldReturnSpell_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "FindAsync_ShouldReturnSpell_WhenPassedValidParameters")
                .Options;

            Guid validId = Guid.NewGuid();

            Spell validSpell = new Spell
            {
                Id = validId,
                Name = "testSpell"
            };

            Spell result = null;

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                await actContext.Spells.AddAsync(validSpell);
                await actContext.SaveChangesAsync();

                SpellService SUT = new SpellService(
                    pandaScoreClientMock.Object,
                    actContext);

                result = await SUT.FindAsync(validId.ToString());
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Spells.Any(c => c.Id.Equals(result.Id)));
                Assert.IsTrue(assertContext.Spells.Any(c => c.Name.Equals(result.Name)));
            }
        }

        [TestMethod]
        public async Task RebaseSpellsAsync_ShouldRepopulateSpellTable_WhenPassedValidParameters()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RebaseSpellsAsync_ShouldRepopulateSpellTable_WhenPassedValidParameters")
                .Options;

            string validAccessToken = string.Empty;
            string validCollectionName = "spells";
            int validPageSize = 100;

            Spell validSpell = new Spell
            {
                Id = Guid.NewGuid(),
                Name = "testTeam",
                DeletedOn = DateTime.UtcNow.AddHours(2),
                IsDeleted = true
            };

            IEnumerable<Spell> validSpellList = new List<Spell>()
            {
                validSpell
            };

            // Act
            using (DataContext actContext = new DataContext(contextOptions))
            {
                Mock<IPandaScoreClient> pandaScoreClientMock = new Mock<IPandaScoreClient>();

                pandaScoreClientMock
                    .Setup(mock => mock.GetEntitiesParallel<Spell>(validAccessToken, validCollectionName, validPageSize))
                    .Returns(Task.FromResult(validSpellList));

                SpellService SUT = new SpellService(
                    pandaScoreClientMock.Object,
                    actContext);

                await SUT.RebaseSpellsAsync(validAccessToken);
            }

            // Assert
            using (DataContext assertContext = new DataContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Spells.Count() == 1);
                Assert.IsTrue(assertContext.Spells.Contains(validSpell));
            }
        }
    }
}
