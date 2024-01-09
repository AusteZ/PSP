using PSP.Models;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class OrderServicesRepository : BaseRepository<OrderServices>
    {
        public OrderServicesRepository(PSPDatabaseContext dbContext) : base(dbContext) { }
    }
}
