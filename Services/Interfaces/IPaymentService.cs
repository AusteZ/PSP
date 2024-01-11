using PSP.Models.DTOs.Output;
using PSP.Models.DTOs.Payments;
using PSP.Models.Entities;
using PSP.Repositories;

namespace PSP.Services.Interfaces
{
    public interface IPaymentService
    {
        public ReceiptOutput PayWithCard(Order order, CardPayment card, int? couponId);
        public ReceiptOutput PayWithCash(Order order, int? couponId);
        public IEnumerable<Receipt> GetAll();
        public Receipt Get(int id);
    }
}
