using PSP.Models;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class OrdersService : CrudEntityService<Order, OrderCreate>
    {
        private readonly ICrudEntityService<OrderProducts, OrderProductsCreate> _orderProductsService;
        private readonly ICrudEntityService<OrderServices, OrderServicesCreate> _orderServicesService;

        public OrdersService(IBaseRepository<Order> repository, 
            ICrudEntityService<OrderProducts, OrderProductsCreate> orderProductService,
            ICrudEntityService<OrderServices, OrderServicesCreate> orderServicesService) 
            : base(repository)
        {
            _orderProductsService = orderProductService;
            _orderServicesService = orderServicesService;
        }

        protected override Order ModelToEntity(OrderCreate entity, int id = 0)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = entity.CustomerId,
                Status = entity.Status == default ? OrderStatus.created : entity.Status,
                StartDate = entity.StartDate == default ? DateTime.Now : entity.StartDate,
                EndDate = entity.EndDate,
            };

            return order;
        }

        public override Order Add(OrderCreate entity)
        {
            var order = base.Add(entity);
            if (entity.OrderProducts != null)
            {
                foreach (var op in entity.OrderProducts)
                {
                    op.OrderId = order.Id;
                    op.Order = order;
                    _orderProductsService.Add(op);
                }
            }
            // Handle OrderServices
            if (entity.OrderServices != null)
            {
                foreach (var os in entity.OrderServices)
                {
                    os.OrderId = order.Id;
                    os.Order = order;
                    _orderServicesService.Add(os);
                }
            }
            return order;
        }
    }
}
