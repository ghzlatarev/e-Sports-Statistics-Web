using ESportStatistics.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESportStatistics.Data.Context.Configurations
{
    internal class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matches");

            builder.HasIndex(m => m.PandaScoreId).IsUnique(true);

            builder.HasOne(m => m.Tournament)
                .WithMany(t => t.Matches)
                .HasForeignKey(m => m.TournamentId)
                .HasPrincipalKey(t => t.PandaScoreId);
        }
    }
}
