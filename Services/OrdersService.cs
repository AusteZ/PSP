using AutoMapper;
using PSP.Models;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class OrdersService : CrudEntityService<Order, OrderCreate>, IOrdersService
    {
        private readonly IBaseRepository<OrderProduct> _orderProductsRepository;
        private readonly IServiceSlotsService _serviceSlotsService;
        private readonly IProductsService _productsService;

        public OrdersService(IBaseRepository<Order> repository,
            IBaseRepository<OrderProduct> orderProductsRepository,
            IServiceSlotsService servicesSlotsService,
            IProductsService productsService,
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
            AddServices(entity.serviceSlotIds.Except(oldServices.Select(x => x.ServiceId)), order);

            RemoveProducts(oldProducts.Select(x => x.ProductId), oldEntity.Id);
            AddProducts(entity.ProductsIds, order);
            return Get(order.Id);
        }

        public override Order Add(OrderCreate entity)
        {
            var order = base.Add(entity);
            AddServices(entity.serviceSlotIds, order);
            AddProducts(entity.ProductsIds, order);
            return order;
        }

        public Order UpdateProperties(int orderId, OrderPatch properties)
        {
            var order = Get(orderId);

            if (properties.Tips != null)
                order.Tips = properties.Tips.Value;

            RemoveServices(properties.ServiceSlotsToRemove);
            AddServices(properties.ServiceSlotsToAdd, order);

            RemoveProducts(properties.ProductsToRemove, order.Id);
            AddProducts(properties.ProductsToAdd, order);

            Update(order);
            return Get(orderId);
        }

        private void AddProducts(IEnumerable<int> products, Order order)
        {
            var productsList = products.ToList();
            for (var i = 0; i < productsList.Count;)
            {
                var productId = productsList[i];
                _productsService.AddToOrder(productId, order.Id, productsList.Count(x => x == productId));
                productsList.RemoveAll(x => x == productId);
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
                _productsService.RemoveFromOrder(product, orderId);
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