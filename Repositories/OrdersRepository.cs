using PSP.Models;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class OrdersRepository : BaseRepository<Order>
    {
        public OrdersRepository(PSPDatabaseContext dbContext) : base(dbContext) { }
    }
}
