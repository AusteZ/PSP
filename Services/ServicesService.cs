using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class ServicesService : CrudEntityService<Service, ServiceCreate>, IServicesService
    {
        private readonly IBaseRepository<ServiceDiscount> _serviceDiscountRepository;
        public ServicesService(IBaseRepository<Service> repository, 
            IBaseRepository<ServiceDiscount> serviceDiscountRepository, 
            IMapper mapper) :
            base(repository, mapper)
        {
            _serviceDiscountRepository = serviceDiscountRepository;
        }

        public void AddToDiscount(int id, int discountId)
        {
            var relationship = _serviceDiscountRepository.Find(id, discountId);
            if (relationship != null)
            {
                return;
            }

            _serviceDiscountRepository.Add(new ServiceDiscount()
            {
                DiscountId = discountId,
                ServiceId = id,
                Service = Get(id),
            });
        }

        public void RemoveFromDiscount(int id, int discountId)
        {
            var relationship = _serviceDiscountRepository.Find(id, discountId);
            if (relationship == null)
            {
                return;
            }

            _serviceDiscountRepository.Remove(relationship);
        }

        protected override Service ModelToEntity(ServiceCreate entity, int id = 0)
        {
            var service = _mapper.Map<Service>(entity);
            service.Id = id;
            return service;
        }

    }
}
