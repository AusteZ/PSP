using Microsoft.EntityFrameworkCore;
using PSP.Models;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class ServiceSlotsRepository : BaseRepository<ServiceSlot>
    {
        private IBaseRepository<Service> _serviceRepository;
        private IBaseRepository<Cancellation> _cancellationRepository;

        public ServiceSlotsRepository(PSPDatabaseContext dbContext, IBaseRepository<Service> serviceRepository, IBaseRepository<Cancellation> cr) :
            base(dbContext)
        {
            _serviceRepository = serviceRepository;
            _cancellationRepository = cr;
        }

        public override ServiceSlot? Find(params int[] ids)
        {
            return _dbSet.Include(e => e.Service).ThenInclude(e => e.Discounts).ThenInclude(e => e.Discount).FirstOrDefault(e => e.Id == ids[0]);
        }

        public override IEnumerable<ServiceSlot> FindAll()
        {
            return _dbSet.Include(e => e.Service).ThenInclude(e => e.Discounts).ThenInclude(e => e.Discount).ToList();
        }

        public override IQueryable<ServiceSlot> GetQueryable()
        {
            return _dbSet.Include(e => e.Service).ThenInclude(e => e.Discounts).ThenInclude(e => e.Discount).AsQueryable();
        }
    }
}
