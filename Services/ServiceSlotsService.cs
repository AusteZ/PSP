using AutoMapper;
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
        private readonly IServicesService _servicesService;

        public ServiceSlotsService(IBaseRepository<ServiceSlot> repository,
            ICrudEntityService<Cancellation, CancellationCreate> cancellationService,
            IServicesService servicesService, IMapper mapper) : base(repository, mapper)
        {
            _cancellationService = cancellationService;
            _servicesService = servicesService;
        }

        public override ServiceSlot Add(ServiceSlotCreate entity)
        {
            var addedEntity = base.Add(entity);
            var parentService = _servicesService.Get(entity.ServiceId);
            parentService.ServiceSlots.Add(addedEntity);
            return addedEntity;
        }

        public override ServiceSlot Update(ServiceSlotCreate creationModel, int id)
        {
            _servicesService.CheckFor404(creationModel.ServiceId);
            return base.Update(creationModel, id);
        }

        public void Book(int id, int orderId)
        {
            var slot = base.Get(id);
            if (slot.OrderId != null)
                throw new UserFriendlyException("This time slot is taken!", 400);

            slot.OrderId = orderId;
            base.Update(slot);
        }

        public void Cancel(int id)
        {
            var slot = base.Get(id);
            var cancellation = new CancellationCreate
            {
                CancellationTime = DateTime.Now,
                ServiceSlotId = id,
                OrderId = slot.OrderId ?? throw new UserFriendlyException("This time slot is free!", 400),
            };

            slot.OrderId = null;
            slot.PartySize = null;

            _cancellationService.Add(cancellation);
            base.Update(slot);
        }

        public IEnumerable<ServiceSlot> GetFiltered(int? employeeId, int? serviceId, bool? isFree)
        {
            var query = _repository.GetQueryable();

            if (isFree != null)
                query = query.Where(e => isFree.Value ? e.OrderId == null : e.OrderId != null);
            if (employeeId != null)
                query = query.Where(e => e.EmployeeId == employeeId);
            if (serviceId != null)
                query = query.Where(e => e.ServiceId == serviceId);

            return query.ToList();
        }

        protected override ServiceSlot ModelToEntity(ServiceSlotCreate entity, int id = 0)
        {
            var serviceSlot = _mapper.Map<ServiceSlot>(entity);
            serviceSlot.Id = id;
            serviceSlot.Service = _servicesService.Get(entity.ServiceId);
            return serviceSlot;
        }
    }
}
