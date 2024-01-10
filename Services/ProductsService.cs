using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;

namespace PSP.Services
{
    public class ProductsService : CrudEntityService<Product, ProductCreate>
    {
        public ProductsService(IBaseRepository<Product> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Product ModelToEntity(ProductCreate entity, int id = 0)
        {

            var product = _mapper.Map<Product>(entity);
            product.Id = id;
            return product;
        }
    }
}
