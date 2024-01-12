using Microsoft.EntityFrameworkCore;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;

namespace PSP.Repositories
{
    public class OrderProductsRepository : BaseRepository<OrderProduct>
    {
        public OrderProductsRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override OrderProduct? Find(params int[] ids)
        {
            return _dbSet.Include(e => e.Order).Include(e => e.Product).FirstOrDefault(e => e.ProductId == ids[0] && e.OrderId == ids[1]);
        }
    }
}
