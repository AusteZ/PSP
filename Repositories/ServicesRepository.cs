using Microsoft.EntityFrameworkCore;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class ServicesRepository : BaseRepository<Service>
    {
        public ServicesRepository(PSPDatabaseContext dbContext) : base(dbContext) { }
    }
}
