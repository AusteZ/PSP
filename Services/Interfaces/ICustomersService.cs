using PSP.Models.DTOs;
using PSP.Models.Entities;

namespace PSP.Services.Interfaces
{
    public interface ICustomersService : ICrudEntityService<Customer, CustomerLogin>
    {
        public Customer Authenticate(CustomerLogin userLoginDto);
    }
}
