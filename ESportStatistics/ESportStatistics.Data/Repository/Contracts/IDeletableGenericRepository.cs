using ESportStatistics.Data.Models.Abstract;
using System.Linq;

namespace ESportStatistics.Data.Repository.Contracts
{
    public interface IDeletableGenericRepository<T>
        : IGenericRepository<T>
        where T : BaseEntity
    {
        IQueryable<T> AllWithDeleted();

        void HardDelete(T entity);
    }
}
