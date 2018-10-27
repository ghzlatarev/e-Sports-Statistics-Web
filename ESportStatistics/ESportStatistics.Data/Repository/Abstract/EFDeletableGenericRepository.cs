using ESportStatistics.Data.Context.Contracts;
using ESportStatistics.Data.Models.Abstract;
using ESportStatistics.Data.Models.Contracts;
using ESportStatistics.Data.Repository.Contracts;
using System;
using System.Linq;

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

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
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
