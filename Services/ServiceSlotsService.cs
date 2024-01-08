using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Models.Exceptions;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class ServiceSlotsService : CrudEntityService<ServiceSlot, ServiceSlotCreate>, IServiceSlotsService
    {
        private readonly ICrudEntityService<Cancellation, CancellationCreate> _cancellationService;
        private readonly ICrudEntityService<Service, ServiceCreate> _servicesService;

        public ServiceSlotsService(IBaseRepository<ServiceSlot> repository,
            ICrudEntityService<Cancellation, CancellationCreate> cancellationService,
            ICrudEntityService<Service, ServiceCreate> servicesService) : base(repository)
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
