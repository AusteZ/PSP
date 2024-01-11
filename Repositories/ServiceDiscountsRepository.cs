using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;

namespace PSP.Repositories
{
    public class ServiceDiscountsRepository : BaseRepository<ServiceDiscount>
    {
        public ServiceDiscountsRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override ServiceDiscount? Find(params int[] ids)
        {
            return _dbSet.Find(ids[0], ids[1]);
        }
    }
}
