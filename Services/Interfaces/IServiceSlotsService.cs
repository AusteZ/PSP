using PSP.Models;

namespace PSP.Services.Interfaces
{
    public interface IServiceSlotsService : IBaseService<ServiceSlot, ServiceSlotCreate>
    {
        public void Book(int id, ServiceSlotBooking booking);
        public void Cancel(int id);
        public IEnumerable<ServiceSlot> GetFiltered(int? employeeId, int? serviceId, bool? isFree);
    }
}
