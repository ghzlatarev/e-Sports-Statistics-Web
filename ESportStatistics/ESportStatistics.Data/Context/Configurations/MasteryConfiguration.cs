using ESportStatistics.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESportStatistics.Data.Context.Configurations
{
    internal class MasteryConfiguration : IEntityTypeConfiguration<Mastery>
    {
        public void Configure(EntityTypeBuilder<Mastery> builder)
        {
            builder.ToTable("Masteries");

            builder.HasIndex(i => i.PandaScoreId).IsUnique(true);
        }
    }
}
