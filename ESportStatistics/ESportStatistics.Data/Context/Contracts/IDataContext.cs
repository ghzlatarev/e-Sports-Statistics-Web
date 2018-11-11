using System;
using ESportStatistics.Data.Models;
using Microsoft.EntityFrameworkCore;
using ESportStatistics.Data.Models.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace ESportStatistics.Data.Context.Contracts
{
    public interface IDataContext : IDisposable
    {
        DbSet<ApplicationUser> Users { get; set; }

        DbSet<Item> Items { get; set; }

        DbSet<Mastery> Masteries { get; set; }

        DbSet<Spell> Spells { get; set; }

        DbSet<Team> Teams { get; set; }

        DbSet<Tournament> Tournaments { get; set; }

        DbSet<League> Leagues { get; set; }

        DbSet<Match> Matches { get; set; }

        DbSet<Champion> Champions { get; set; }

        DbSet<Player> Players { get; set; }

        DbSet<Serie> Series { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(bool applyDeletionRules, bool applyAuditInfoRules);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry Entry(object entity);
    }
}
