using Microsoft.EntityFrameworkCore;
using PSP.Models;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class ProductsRepository : BaseRepository<Product>
    {
        public ProductsRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override Product? Find(params int[] ids)
        {
            return _dbSet.Include(e => e.Orders).ThenInclude(e => e.Order).Include(e => e.Discounts).ThenInclude(e => e.Discount).FirstOrDefault(e => e.Id == ids[0]);
        }

        public override IEnumerable<Product> FindAll()
        {
            return _dbSet.Include(e => e.Orders).ThenInclude(e => e.Order).Include(e => e.Discounts).ThenInclude(e => e.Discount).ToList();
        }
    }
}
