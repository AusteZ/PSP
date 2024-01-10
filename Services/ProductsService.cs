using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class ProductsService : CrudEntityService<Product, ProductCreate>, IProductsService
    {
        private readonly IBaseRepository<OrderProduct> _orderProductsRepository;

        public ProductsService(
            IBaseRepository<Product> repository,
            IMapper mapper,
            IBaseRepository<OrderProduct> orderProductsRepository) : base(repository, mapper)
        {
            _orderProductsRepository = orderProductsRepository;
        }

        public void AddToOrder(int id, int orderId, int quantity)
        {
            var relationship = _orderProductsRepository.Find(id, orderId);
            if (relationship != null)
            {
                relationship.Quantity += quantity;
                _orderProductsRepository.Update(relationship);
                return;
            }

            _orderProductsRepository.Add(new OrderProduct()
            {
                OrderId = orderId,
                Quantity = quantity,
                ProductId = id,
                Product = Get(id),
            });
        }

        public void RemoveFromOrder(int id, int orderId, int quantity = -1)
        {
            var relationship = _orderProductsRepository.Find(id, orderId);
            if (relationship == null)
                return;

            if (quantity < 0)
                quantity = relationship.Quantity;

            relationship.Quantity -= quantity;
            if (relationship.Quantity <= 0)
                _orderProductsRepository.Remove(relationship);
            else
                _orderProductsRepository.Update(relationship);
        }

        protected override Product ModelToEntity(ProductCreate entity, int id = 0)
        {

            var product = _mapper.Map<Product>(entity);
            product.Id = id;
            return product;
        }
    }
}
