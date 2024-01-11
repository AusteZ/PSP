using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class CustomersService : CrudEntityService<Customer, CustomerCreate>, ICustomersService
    {
        public CustomersService(IBaseRepository<Customer> repository, IMapper mapper) : base(repository, mapper)
        { }

        public Customer Authenticate(CustomerLogin customerLoginDto)
        {
            var currentUser = _repository.GetQueryable().SingleOrDefault(user => user.Username.ToLower() ==
                customerLoginDto.Username.ToLower() && user.Password == customerLoginDto.Password);

            return currentUser;
        }

        protected override Customer ModelToEntity(CustomerCreate dto, int id = 0)
        {
            var customer = _mapper.Map<Customer>(dto);
            customer.Id = id;
            return customer;
        }
    }
}