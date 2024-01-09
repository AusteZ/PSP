using PSP.Models;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class OrderProductsRepository : BaseRepository<OrderProducts>
    {
        public OrderProductsRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override OrderProducts Add(OrderProducts entity)
        {
            entity.Product = _dbContext.Find<Product>(entity.ProductId);
            var addedEntity = _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return addedEntity.Entity;
        }
    }
}
