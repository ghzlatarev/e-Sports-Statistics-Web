using ESportStatistics.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESportStatistics.Data.Context.Configurations
{
    internal class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.ToTable("Leagues");

            builder.HasIndex(i => i.PandaScoreId).IsUnique(true);
        }
    }
}
