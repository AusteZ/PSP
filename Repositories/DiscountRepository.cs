using Microsoft.EntityFrameworkCore;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class DiscountRepository : BaseRepository<Discount>
    {
        public DiscountRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override Discount? Find(params int[] ids)
        {
            return _dbSet.Include(e => e.Products).ThenInclude(e => e.Product)
                .Include(e => e.Services).ThenInclude(e => e.Service).FirstOrDefault(e => e.Id == ids[0]);
        }

        public override IEnumerable<Discount> FindAll()
        {
            return _dbSet.Include(e => e.Products).ThenInclude(e => e.Product)
                .Include(e => e.Services).ThenInclude(e => e.Service).ToList();
        }
    }
}
