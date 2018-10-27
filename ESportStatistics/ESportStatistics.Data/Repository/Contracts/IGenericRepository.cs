using System.Linq;
using System.Collections.Generic;
using ESportStatistics.Data.Models.Abstract;

namespace ESportStatistics.Data.Repository.Contracts
{
    public interface IGenericRepository<T>
        where T : BaseEntity
    {
        IQueryable<T> All();

        T GetById(int id);

        void Add(T entity);

        void Add(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Detach(T entity);
    }
}
