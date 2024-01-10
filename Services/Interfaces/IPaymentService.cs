using PSP.Models.DTOs.Output;
using PSP.Models.DTOs.Payments;

namespace PSP.Services.Interfaces
{
    public interface IPaymentService
    {
        public ReceiptOutput PayWithCard(OrderOutput order, CardPayment card);
        public ReceiptOutput PayWithCash(OrderOutput order);
    }
}
