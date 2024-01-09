using PSP.Models;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;

namespace PSP.Services
{
    public class OrderProductsService : CrudEntityService<OrderProducts, OrderProductsCreate>
    {
        public OrderProductsService(IBaseRepository<OrderProducts> repository) : base(repository)
        { 
        }

        protected override OrderProducts ModelToEntity(OrderProductsCreate entity, int id = 0)
        {
            return new OrderProducts()
            {
                OrderId = id,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity,
            };
        }
    }
}
