using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class DiscountService : CrudEntityService<Discount, DiscountCreate>
    {
        private readonly IProductsService _productsService;
        private readonly IServicesService _servicesService;
        public DiscountService(IBaseRepository<Discount> repository,
            IProductsService productService,
            IServicesService serviceRepository,
            IMapper mapper):base(repository, mapper) 
        {
            _productsService = productService;
            _servicesService = serviceRepository;
        }
        protected override Discount ModelToEntity(DiscountCreate entity, int id = 0)
        {
            var discount = _mapper.Map<Discount>(entity);
            discount.Id = id;
            return discount;
        }

        public override Discount Update(DiscountCreate entity, int id)
        {
            var oldEntity = Get(id);
            var oldServices = oldEntity.Services.ToList();
            var oldProducts = oldEntity.Products.ToList();
            var discount = base.Update(entity, id);

            RemoveServices(oldServices.Select(x => x.ServiceId), oldEntity.Id);
            AddServices(entity.ServiceIds, discount);

            RemoveProducts(oldProducts.Select(x => x.ProductId), oldEntity.Id);
            AddProducts(entity.ProductIds, discount);

            return Get(discount.Id);
        }

        public override Discount Add(DiscountCreate entity)
        {
            var discount = base.Add(entity);
            AddServices(entity.ServiceIds, discount);
            AddProducts(entity.ProductIds, discount);
            return discount;
        }

        private void AddProducts(IEnumerable<int> products, Discount discount)
        {
            var productsList = products.ToList();
            for (var i = 0; i < productsList.Count;)
            {
                _productsService.AddToDiscount(productsList[i], discount.Id);
                productsList.RemoveAll(x => x == productsList[i]);
            }
        }

        private void RemoveProducts(IEnumerable<int> products, int discountId)
        {
            foreach (var product in products)
            {
                _productsService.RemoveFromDiscount(product, discountId);
            }
        }

        private void AddServices(IEnumerable<int> services, Discount discount)
        {
            var serviceList = services.ToList();
            for (var i = 0; i < serviceList.Count;)
            {
                _servicesService.AddToDiscount(serviceList[i], discount.Id);
                serviceList.RemoveAll(x => x == serviceList[i]);
            }
        }

        private void RemoveServices(IEnumerable<int> services, int discountId)
        {
            foreach (var product in services)
            {
                _servicesService.RemoveFromDiscount(product, discountId);
            }
        }
    }
}
