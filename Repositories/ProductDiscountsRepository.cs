using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;

namespace PSP.Repositories
{
    public class ProductDiscountsRepository : BaseRepository<ProductDiscount>
    {
        public ProductDiscountsRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override ProductDiscount? Find(params int[] ids)
        {
            return _dbSet.Find(ids[0], ids[1]);
        }
    }
}
