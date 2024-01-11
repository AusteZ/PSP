using PSP.Models.DTOs.Output;
using PSP.Models.DTOs.Payments;
using PSP.Models.Entities;
using PSP.Repositories;

namespace PSP.Services.Interfaces
{
    public interface IPaymentService
    {
        public ReceiptOutput PayWithCard(OrderOutput order, CardPayment card, int? tip = null);
        public ReceiptOutput PayWithCash(OrderOutput order, int? tip = null);
        public IEnumerable<Receipt> GetAll();
        public Receipt Get(int id);
    }
}
