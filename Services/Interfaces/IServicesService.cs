using PSP.Models.DTOs;
using PSP.Models.Entities;

namespace PSP.Services.Interfaces
{
    public interface IServicesService : ICrudEntityService<Service, ServiceCreate>
    {
        public void AddToDiscount(int id, int discountId);
        public void RemoveFromDiscount(int id, int discountId);

    }
}
