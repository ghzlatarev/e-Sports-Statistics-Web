using ESportStatistics.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESportStatistics.Data.Context.Configurations
{
    internal class ChampionConfiguration : IEntityTypeConfiguration<Champion>
    {
        public void Configure(EntityTypeBuilder<Champion> builder)
        {
            builder.ToTable("Champions");

            builder.HasIndex(i => i.PandaScoreId).IsUnique(true);

        }
    }
}
