using ESportStatistics.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Data.Repository.Contracts
{
    public interface IGenericRepository<T>
        where T : BaseEntity
    {
        IQueryable<T> All();

        T GetById(Guid id);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        Task AddAsync(T entity);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(Guid id);

        void Detach(T entity);
    }
}
