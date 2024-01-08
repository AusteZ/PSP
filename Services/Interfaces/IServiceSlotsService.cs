using PSP.Models.DTOs;
using PSP.Models.Entities;

namespace PSP.Services.Interfaces
{
    public interface IServiceSlotsService : ICrudEntityService<ServiceSlot, ServiceSlotCreate>
    {
        public void Book(int id, ServiceSlotBooking booking);
        public void Cancel(int id);
        public IEnumerable<ServiceSlot> GetFiltered(int? employeeId, int? serviceId, bool? isFree);
    }
}
