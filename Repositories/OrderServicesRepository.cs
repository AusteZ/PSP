using PSP.Models;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class OrderServicesRepository : BaseRepository<OrderServices>
    {
        public OrderServicesRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override OrderServices Add(OrderServices entity)
        {
            entity.Service = _dbContext.Find<Service>(entity.ServiceId);
            var addedEntity = _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return addedEntity.Entity;
        }
    }
}
