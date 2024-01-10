using PSP.Models;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class CustomersRepository : BaseRepository<Customer>
    {
        public CustomersRepository(PSPDatabaseContext dbContext) : base(dbContext) { }
    }
}