using ESportStatistics.Data.Context.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Data.Models.Abstract;
using ESportStatistics.Data.Models.Contracts;
using ESportStatistics.Data.Repository.Abstract;
using ESportStatistics.Data.Repository.Contracts;
using ESportStatistics.Data.Repository.DataHandler.Contracts;
using System;
using System.Collections.Generic;

namespace ESportStatistics.Data.Repository.DataHandler
{
    public class DataHandler : IDataHandler
    {
        private readonly IDataContext context;

        private readonly IDictionary<Type, object> repositories;

        public DataHandler(IDataContext context)
        {
            this.context = context ?? throw new ArgumentNullException();

            this.repositories = new Dictionary<Type, object>();
        }

        public IDeletableGenericRepository<Spell> Spells
        {
            get
            {
                return this.GetDeletableEntityRepository<Spell>();
            }
        }

        public IDeletableGenericRepository<Item> Items
        {
            get
            {
                return this.GetDeletableEntityRepository<Item>();
            }
        }

        public IDeletableGenericRepository<Champion> Champions
        {
            get
            {
                return this.GetDeletableEntityRepository<Champion>();
            }
        }

        public IDeletableGenericRepository<Serie> Series
        {
            get
            {
                return this.GetDeletableEntityRepository<Serie>();
            }
        }

        public IDeletableGenericRepository<Mastery> Masteries
        {
            get
            {
                return this.GetDeletableEntityRepository<Mastery>();
            }
        }

        public IDeletableGenericRepository<Player> Players
        {
            get
            {
                return this.GetDeletableEntityRepository<Player>();
            }
        }

        public IDeletableGenericRepository<Team> Teams
        {
            get
            {
                return this.GetDeletableEntityRepository<Team>();
            }
        }

        public IDeletableGenericRepository<Tournament> Tournaments
        {
            get
            {
                return this.GetDeletableEntityRepository<Tournament>();
            }
        }

        public IDeletableGenericRepository<League> Leagues
        {
            get
            {
                return this.GetDeletableEntityRepository<League>();
            }
        }

        public IDeletableGenericRepository<Match> Matches
        {
            get
            {
                return this.GetDeletableEntityRepository<Match>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>()
            where T : BaseEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EFGenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableGenericRepository<T> GetDeletableEntityRepository<T>()
            where T : BaseEntity, IDeletable
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EFDeletableGenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableGenericRepository<T>)this.repositories[typeof(T)];
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context?.Dispose();
            }
        }
    }
}
