using ESportStatistics.Data.Models.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Data.Repository.Contracts
{
    public interface IGenericRepository<T>
        where T : BaseEntity
    {
        IQueryable<T> All();

        T GetById(int id);

        void Add(T entity);

        Task AddAsync(T entity);

        void Add(IEnumerable<T> entities);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Detach(T entity);
    }
}
