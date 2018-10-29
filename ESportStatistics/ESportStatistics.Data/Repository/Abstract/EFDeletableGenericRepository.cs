using ESportStatistics.Data.Context.Contracts;
using ESportStatistics.Data.Models.Abstract;
using ESportStatistics.Data.Models.Contracts;
using ESportStatistics.Data.Repository.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Data.Repository.Abstract
{
    public class EFDeletableGenericRepository<T>
        : EFGenericRepository<T>, IDeletableGenericRepository<T>
        where T : BaseEntity, IDeletable
    {
        public EFDeletableGenericRepository(IDataContext context) : base(context)
        {

        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public async Task<IQueryable<T>> AllWithDeletedAsync()
        {
            return await base.AllAsync();
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public override async Task<IQueryable<T>> AllAsync()
        {
            return (await base.AllAsync()).Where(x => !x.IsDeleted);
        }

        public override void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            base.Update(entity);
        }

        public void HardDelete(T entity)
        {
            base.Delete(entity);
        }
    }
}
