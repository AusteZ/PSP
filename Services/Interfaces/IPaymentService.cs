using PSP.Models.DTOs;
using PSP.Models.DTOs.Output;
using PSP.Models.Entities;
using PSP.Repositories;

namespace PSP.Services.Interfaces
{
    public interface IPaymentService
    {
        public Receipt PayWithCard(int orderId, CardPayment card, int? couponId);
        public Receipt PayWithCash(int orderId, int? couponId);
        public IEnumerable<Receipt> GetAll();
        public Receipt Get(int id);
    }
}
