using Microsoft.EntityFrameworkCore;
using PSP.Models;

namespace PSP.Repositories
{
    public class ServiceSlotsRepository : BaseRepository<ServiceSlot>
    {
        public ServiceSlotsRepository(PSPDatabaseContext dbContext) : base(dbContext) { }
    }
}
