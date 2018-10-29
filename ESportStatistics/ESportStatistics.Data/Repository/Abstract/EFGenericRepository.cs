using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ESportStatistics.Data.Context.Contracts;
using ESportStatistics.Data.Models.Abstract;
using ESportStatistics.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace ESportStatistics.Data.Repository.Abstract
{
    public class EFGenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        public EFGenericRepository(IDataContext context)
        {
            this.Context = context ?? throw new ArgumentNullException();
            this.DbSet = Context.Set<T>();
        }

        protected IDataContext Context { get; }

        protected DbSet<T> DbSet { get; }

        public virtual IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        public virtual async Task<IQueryable<T>> AllAsync()
        {
            return (await this.DbSet.ToListAsync()).AsQueryable();
        }

        public virtual T GetById(int id)
        {
            return this.DbSet.Find(id);
        }
        
        public void Add(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public async Task AddAsync(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                await this.DbSet.AddAsync(entity);
            }
        }

        public void Add(IEnumerable<T> entities)
        {
            this.DbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public virtual void Detach(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
