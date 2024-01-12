using PSP.Models.DTOs;
using PSP.Models.Entities;

namespace PSP.Services.Interfaces
{
    public interface IOrdersService : ICrudEntityService<Order, OrderCreate>
    {
        public Order UpdateProperties(int orderId, OrderPatch properties);
    }
}
