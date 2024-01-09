using PSP.Models;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;

namespace PSP.Services
{
    public class OrderServicesService : CrudEntityService<OrderServices, OrderServicesCreate>
    {
        public OrderServicesService(IBaseRepository<OrderServices> repository) : base(repository)
        { 
        }

        protected override OrderServices ModelToEntity(OrderServicesCreate entity, int id = 0)
        {
            return new OrderServices()
            {
                OrderId = entity.ServiceId,
                ServiceId = entity.ServiceId,
                Order = entity.Order,
                Service = entity.Service
            };
        }
    }
}
