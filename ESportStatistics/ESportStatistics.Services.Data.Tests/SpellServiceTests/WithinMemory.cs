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
    }
}
