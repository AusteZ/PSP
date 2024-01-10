using Microsoft.EntityFrameworkCore;
using PSP.Models.Entities;
using System.Collections.Generic;

namespace PSP.Repositories
{
    public class OrdersRepository : BaseRepository<Order>
    {
        public OrdersRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override Order? Find(params int[] ids)
        {
            return _dbSet.Include(e => e.Products).ThenInclude(e => e.Product)
                .Include(e => e.ServiceSlots).ThenInclude(e => e.ServiceSlot).ThenInclude(e => e.Service).FirstOrDefault(e => e.Id == ids[0]);
        }

        public override IEnumerable<Order> FindAll()
        {
            return _dbSet.Include(e => e.Products).ThenInclude(e => e.Product)
                .Include(e => e.ServiceSlots).ThenInclude(e => e.ServiceSlot).ThenInclude(e => e.Service).ToList();
        }
    }
}
