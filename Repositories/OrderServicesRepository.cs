using Microsoft.EntityFrameworkCore;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class OrderServicesRepository : BaseRepository<OrderService>
    {
        public OrderServicesRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override OrderService? Find(params int[] ids)
        {
            return _dbSet.Find(ids[0], ids[1]);
        }
    }
}
