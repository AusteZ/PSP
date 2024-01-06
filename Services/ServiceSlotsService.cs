using Microsoft.EntityFrameworkCore;
using PSP.Models;
using PSP.Models.RequestBodies;

namespace PSP.Services
{
    public class ServiceSlotsService : BaseService<ServiceSlot, ServiceSlotCreate>
    {
        private readonly CancellationService _cancellationService;

        public ServiceSlotsService(PSPDatabaseContext db, CancellationService cancellationService) : base(db)
        {
            _cancellationService = cancellationService;
        }

        public void Book(int id, ServiceSlotBooking booking)
        {
            var slot = base.Get(id);
            if (slot.CustomerId != null)
                throw new UserFriendlyException("This time slot is taken!", 400);

            slot.CustomerId = booking.CustomerId;
            slot.PartySize = booking.PartySize;
            base.Update(slot);
        }

        public void Cancel(int id)
        {
            var slot = base.Get(id);
            slot.CustomerId = null;
            slot.PartySize = null;

            var cancellation = new CancellationCreate
            {
                CancellationTime = DateTime.Now,
                ServiceSlotId = id,
            };
            _cancellationService.Add(cancellation);
            base.Update(slot);
        }

        public IEnumerable<ServiceSlot> GetFiltered(int? employeeId, int? serviceId, bool isFree)
        {
            var query = _dbSet.AsQueryable();

            if (isFree)
                query = query.Where(e => e.CustomerId == null);
            if (employeeId != null)
                query = query.Where(e => e.EmployeeId == employeeId);
            if (serviceId != null)
                query = query.Where(e => e.ServiceId == serviceId);

            return query.ToList();
        }

        protected override ServiceSlot ModelToEntity(ServiceSlotCreate entity, int id = 0)
        {
            return new ServiceSlot()
            {
                Id = id,
                ServiceId = entity.ServiceId,
                CustomerId = entity.CustomerId,
                PartySize = entity.PartySize,
                Time = entity.Time,
                EmployeeId = entity.EmployeeId,
                Completed = entity.Completed,
            };
        }
    }
}
