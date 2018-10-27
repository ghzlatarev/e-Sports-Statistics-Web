using ESportStatistics.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESportStatistics.Data.Context.Configurations
{
    internal class SpellConfiguration : IEntityTypeConfiguration<Spell>
    {
        public void Configure(EntityTypeBuilder<Spell> builder)
        {
            builder.ToTable("Spells");

            builder.HasIndex(i => i.PandaScoreId).IsUnique(true);
        }
    }
}
