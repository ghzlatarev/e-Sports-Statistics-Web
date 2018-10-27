using ESportStatistics.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESportStatistics.Data.Context.Configurations
{
    internal class SeriesConfiguration : IEntityTypeConfiguration<Serie>
    {
        public void Configure(EntityTypeBuilder<Serie> builder)
        {
            builder.ToTable("Series");

            builder.HasIndex(i => i.PandaScoreId).IsUnique(true);

            builder.HasOne(s => s.League)
                .WithMany(l => l.Series)
                .HasForeignKey(s => s.LeagueId)
                .HasPrincipalKey(l => l.PandaScoreId);
        }
    }
}
