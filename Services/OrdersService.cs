using AutoMapper;
using PSP.Models;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class OrdersService : CrudEntityService<Order, OrderCreate>
    {
        private readonly IBaseRepository<OrderProduct> _orderProductsRepository;
        private readonly IServiceSlotsService _serviceSlotsService;
        private readonly ICrudEntityService<Product, ProductCreate> _productsService;

        public OrdersService(IBaseRepository<Order> repository,
            IBaseRepository<OrderProduct> orderProductsRepository,
            IServiceSlotsService servicesSlotsService,
            ICrudEntityService<Product, ProductCreate> productsService,
            IMapper mapper)
            : base(repository, mapper)
        {
            _orderProductsRepository = orderProductsRepository;
            _serviceSlotsService = servicesSlotsService;
            _productsService = productsService;
        }

        protected override Order ModelToEntity(OrderCreate entity, int id = 0)
        {
            var order = _mapper.Map<Order>(entity);
            order.Id = id;
            return order;
        }

        public override Order Update(OrderCreate entity, int id)
        {
            var oldEntity = Get(id);
            var oldServices = oldEntity.ServiceSlots.ToList();
            var oldProducts = oldEntity.Products.ToList();
            var order = base.Update(entity, id);

            RemoveServices(oldServices.Select(x => x.ServiceId).Except(entity.serviceSlotIds));
            RemoveProducts(oldProducts.Select(x => x.ProductId).Except(entity.ProductsIds), oldEntity.Id);
            AddServices(entity.serviceSlotIds.Except(oldServices.Select(x => x.ServiceId)), order);
            AddProducts(entity.ProductsIds.Except(oldProducts.Select(x => x.ProductId)), order);
            return Get(order.Id); ;
        }

        public override Order Add(OrderCreate entity)
        {
            var order = base.Add(entity);
            AddServices(entity.serviceSlotIds, order);
            AddProducts(entity.ProductsIds, order);
            return order;
        }

        private void AddProducts(IEnumerable<int> products, Order order)
        {
            foreach (var productId in products)
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

        private void AddServices(IEnumerable<int> services, Order order)
        {
            foreach (var serviceId in services)
            {
                _serviceSlotsService.Book(serviceId, order.Id);
            }
        }

        private void RemoveProducts(IEnumerable<int> products, int orderId)
        {
            foreach (var product in products)
            {
                var connectingTable = _orderProductsRepository.Find(product, orderId);
                if (connectingTable != null)
                    _orderProductsRepository.Remove(connectingTable);
            }
        }

        private void RemoveServices(IEnumerable<int> serviceSlots)
        {
            foreach (var serviceSlot in serviceSlots)
            {
                _serviceSlotsService.Cancel(serviceSlot);
            }
        }
    }
}