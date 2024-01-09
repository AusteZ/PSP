using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public class CustomersService : CrudEntityService<Customer, CustomerLogin>, ICustomersService
    {
        public CustomersService(IBaseRepository<Customer> repository) : base(repository)
        { }

        public Customer Authenticate(CustomerLogin customerLoginDto)
        {
            var currentUser = _repository.GetQueryable().SingleOrDefault(user => user.Username.ToLower() ==
                customerLoginDto.Username.ToLower() && user.Password == customerLoginDto.Password);
            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }

        protected override Customer ModelToEntity(CustomerLogin dto, int id = 0)
        {
            return new Customer()
            {
                Id = id,
                Username = dto.Username,
                Password = dto.Password,
            };
        }
    }
}