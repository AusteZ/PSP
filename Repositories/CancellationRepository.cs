using Microsoft.EntityFrameworkCore;
using PSP.Models;

namespace PSP.Repositories
{
    public class CancellationRepository : BaseRepository<Cancellation>
    {
        public CancellationRepository(PSPDatabaseContext dbContext) : base(dbContext) { }
    }
}
