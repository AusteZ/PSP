using Microsoft.EntityFrameworkCore;
using PSP.Models;

namespace PSP.Services
{
    public class ServicesService : BaseService<Service, ServiceCreate>
    {
        public ServicesService(PSPDatabaseContext db) : base(db)
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
