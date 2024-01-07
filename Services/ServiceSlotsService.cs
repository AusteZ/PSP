using Microsoft.EntityFrameworkCore;
using PSP.Models;
using PSP.Models.RequestBodies;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class ServiceSlotsService : BaseService<ServiceSlot, ServiceSlotCreate>, IServiceSlotsService
    {
        private readonly IBaseService<Cancellation, CancellationCreate> _cancellationService;
        private readonly IBaseService<Service, ServiceCreate> _servicesService;

        public ServiceSlotsService(IBaseRepository<ServiceSlot> repository,
            IBaseService<Cancellation, CancellationCreate> cancellationService,
            IBaseService<Service, ServiceCreate> servicesService) : base(repository)
        {
            _cancellationService = cancellationService;
            _servicesService = servicesService;
        }

        public override ServiceSlot Add(ServiceSlotCreate entity)
        {
            _servicesService.Get(entity.ServiceId);
            return base.Add(entity);
        }

        public override ServiceSlot Update(ServiceSlotCreate creationModel, int id)
        {
            _servicesService.Get(creationModel.ServiceId);
            return base.Update(creationModel, id);
        }

        public override ServiceSlot Update(ServiceSlot entity)
        {
            _servicesService.Get(entity.ServiceId);
            return base.Update(entity);
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
            var cancellation = new CancellationCreate
            {
                CancellationTime = DateTime.Now,
                ServiceSlotId = id,
                CustomerId = slot.CustomerId ?? throw new UserFriendlyException("This time slot is free!", 400),
            };

            slot.CustomerId = null;
            slot.PartySize = null;

            _cancellationService.Add(cancellation);
            base.Update(slot);
        }

        public IEnumerable<ServiceSlot> GetFiltered(int? employeeId, int? serviceId, bool? isFree)
        {
            var query = _repository.GetQueryable();

            if (isFree != null)
                query = query.Where(e => isFree.Value ? e.CustomerId == null : e.CustomerId != null);
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
