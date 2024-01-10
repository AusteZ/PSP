using AutoMapper;
using PSP.Models;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class OrdersService : CrudEntityService<Order, OrderCreate>
    {
        private readonly IBaseRepository<OrderProduct> _orderProductsRepository;
        private readonly IBaseRepository<OrderService> _orderServicesRepository;
        private readonly IServiceSlotsService _serviceSlotsService;
        private readonly ICrudEntityService<Product, ProductCreate> _productsService;

        public OrdersService(IBaseRepository<Order> repository,
            IBaseRepository<OrderProduct> orderProductsRepository,
            IBaseRepository<OrderService> orderServicesRepository,
            IServiceSlotsService servicesSlotsService,
            ICrudEntityService<Product, ProductCreate> productsService,
            IMapper mapper)
            : base(repository, mapper)
        {
            _orderProductsRepository = orderProductsRepository;
            _orderServicesRepository = orderServicesRepository;
            _serviceSlotsService = servicesSlotsService;
            _productsService = productsService;
        }

        protected override Order ModelToEntity(OrderCreate entity, int id = 0)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = entity.CustomerId,
                Status = entity.Status,
                StartDate = entity.StartDate == default ? DateTime.Now : entity.StartDate,
                EndDate = entity.EndDate,
            };

            return order;
        }

        public override Order Update(OrderCreate entity, int id)
        {
            var oldEntity = Get(id);
            var oldServices = oldEntity.ServiceSlots.ToList();
            var oldProducts = oldEntity.Products.ToList();
            var order = base.Update(entity, id);
            RemoveServices(oldServices, oldEntity.Id);
            RemoveProducts(oldProducts, oldEntity.Id);
            AddServices(order, entity);
            AddProducts(order, entity);
            return order;
        }

        public override Order Add(OrderCreate entity)
        {
            var order = base.Add(entity);
            AddServices(order, entity);
            AddProducts(order, entity);
            return order;
        }

        private void AddProducts(Order order, OrderCreate entity)
        {
            foreach (var productId in entity.ProductsIds)
            {
                var orderService = new OrderProduct()
                {
                    OrderId = order.Id,
                    ProductId = productId,
                    Product = _productsService.Get(productId),
                };
                _orderProductsRepository.Add(orderService);
            }
        }

        private void AddServices(Order order, OrderCreate entity)
        {
            foreach (var serviceId in entity.ServiceIds)
            {
                var orderService = new OrderService()
                {
                    OrderId = order.Id,
                    ServiceSlotId = serviceId,
                    ServiceSlot = _serviceSlotsService.Get(serviceId),
                };
                _orderServicesRepository.Add(orderService);
            }
        }

        private void RemoveProducts(IList<OrderProduct> products, int orderId)
        {
            foreach (var product in products)
            {
                var connectingTable = _orderProductsRepository.Find(product.ProductId, orderId);
                if (connectingTable != null)
                    _orderProductsRepository.Remove(connectingTable);
            }
        }

        private void RemoveServices(IList<OrderService> serviceSlots, int orderId)
        {
            foreach (var serviceSlot in serviceSlots)
            {
                var connectingTable = _orderServicesRepository.Find(serviceSlot.ServiceSlotId, orderId);
                if (connectingTable != null)
                    _orderServicesRepository.Remove(connectingTable);
            }
        }
    }
}