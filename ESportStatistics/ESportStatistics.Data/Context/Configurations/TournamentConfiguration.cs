using ESportStatistics.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESportStatistics.Data.Context.Configurations
{
    internal class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.ToTable("Tournaments");

            builder.HasIndex(i => i.PandaScoreId).IsUnique(true);

            builder.HasOne(t => t.Serie)
                .WithMany(s => s.Tournaments)
                .HasForeignKey(t => t.SerieId)
                .HasPrincipalKey(s => s.PandaScoreId);

            builder.HasOne(t => t.League)
                .WithMany(l => l.Tournaments)
                .HasForeignKey(t => t.LeagueId)
                .HasPrincipalKey(l => l.PandaScoreId);
        }
    }
}
