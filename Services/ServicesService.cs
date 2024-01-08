using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;

namespace PSP.Services
{
    public class ServicesService : CrudEntityService<Service, ServiceCreate>
    {
        public ServicesService(IBaseRepository<Service> repository) : base(repository)
        { }

        protected override Service ModelToEntity(ServiceCreate entity, int id = 0)
        {
            return new Service()
            {
                Id = id,
                EuroCost = entity.EuroCost,
                MinutesLength = entity.MinutesLength,
                ServiceName = entity.ServiceName,
                ServiceDescription = entity.ServiceDescription,
            };
        }
    }
}
