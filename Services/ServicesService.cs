using Microsoft.EntityFrameworkCore;
using PSP.Models;
using PSP.Repositories;

namespace PSP.Services
{
    public class ServicesService : BaseService<Service, ServiceCreate>
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
