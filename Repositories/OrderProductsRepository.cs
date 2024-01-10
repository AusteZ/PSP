using Microsoft.EntityFrameworkCore;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class OrderProductsRepository : BaseRepository<OrderProduct>
    {
        public OrderProductsRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override OrderProduct? Find(params int[] ids)
        {
            return _dbSet.Find(ids[0], ids[1]);
        }
    }
}
