﻿using ESportStatistics.Data.Context.Configurations;
using ESportStatistics.Data.Context.Configurations.Identity;
using ESportStatistics.Data.Context.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Models.Contracts;
using ESportStatistics.Data.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Data.Context
{
    public class DataContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Champion> Champions { get; set; }

        public virtual DbSet<Player> Players { get; set; }

        public virtual DbSet<Item> Items { get; set; }

        public virtual DbSet<Mastery> Masteries { get; set; }

        public virtual DbSet<Spell> Spells { get; set; }

        public virtual DbSet<League> Leagues { get; set; }

        public virtual DbSet<Serie> Series { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<Tournament> Tournaments { get; set; }

        public virtual DbSet<Match> Matches { get; set; }

        public override int SaveChanges()
        {
            this.ApplyDeletionRules();
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync(bool applyDeletionRules = true, bool applyAuditInfoRules = true)
        {
            if (applyDeletionRules == true)
            {
                this.ApplyDeletionRules();
            }

            if (applyAuditInfoRules == true)
            {
                this.ApplyAuditInfoRules();
            }

            return await base.SaveChangesAsync();
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Identity model configuration
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());

            // PandaScore model configuration
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new MasteryConfiguration());
            modelBuilder.ApplyConfiguration(new SpellConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new TournamentConfiguration());
            modelBuilder.ApplyConfiguration(new LeagueConfiguration());
            modelBuilder.ApplyConfiguration(new ChampionConfiguration());
            modelBuilder.ApplyConfiguration(new SeriesConfiguration());
            modelBuilder.ApplyConfiguration(new MatchConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyDeletionRules()
        {
            var entitiesForDeletion = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is IDeletable);

            foreach (var entry in entitiesForDeletion)
            {
                var entity = (IDeletable)entry.Entity;
                entity.DeletedOn = DateTime.UtcNow.AddHours(2);
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.UtcNow.AddHours(2);
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow.AddHours(2);
                }
            }
        }
    }
}