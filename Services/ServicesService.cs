using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class ServicesService : CrudEntityService<Service, ServiceCreate>
    {
        public ServicesService(IBaseRepository<Service> repository, IMapper mapper) :
            base(repository, mapper)
        { }

        protected override Service ModelToEntity(ServiceCreate entity, int id = 0)
        {
            var service = _mapper.Map<Service>(entity);
            service.Id = id;
            return service;
        }
    }
}
