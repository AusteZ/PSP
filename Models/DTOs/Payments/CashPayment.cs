using PSP.Models.Entities.RelationalTables;

namespace PSP.Models.DTOs.Payments
{
    public class CashPayment
    {
        public decimal Amount { get; set; }
        public Receipt Receipt { get; set; }
    }
}
