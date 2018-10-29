using ESportStatistics.Data.Models.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Data.Repository.Contracts
{
    public interface IDeletableGenericRepository<T>
        : IGenericRepository<T>
        where T : BaseEntity
    {
        IQueryable<T> AllWithDeleted();

        Task<IQueryable<T>> AllWithDeletedAsync();

        void HardDelete(T entity);
    }
}
