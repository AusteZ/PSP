using PSP.Models.DTOs.Output;
using PSP.Models.DTOs.Payments;
using PSP.Models.Entities;
using PSP.Repositories;

namespace PSP.Services.Interfaces
{
    public interface IPaymentService
    {
        public ReceiptOutput PayWithCard(OrderOutput order, CardPayment card, float tip);
        public ReceiptOutput PayWithCash(OrderOutput order, float tip);
        public IEnumerable<Receipt> GetAll();
        public Receipt Get(int id);
    }
}
