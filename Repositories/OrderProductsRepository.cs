using PSP.Models;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class OrderProductsRepository : BaseRepository<OrderProducts>
    {
        public OrderProductsRepository(PSPDatabaseContext dbContext) : base(dbContext) { }
    }
}
