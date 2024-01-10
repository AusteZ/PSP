namespace PSP.Models.Entities.RelationalTables
{
    public class Receipt
    {
        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public decimal Total { get; set; }
        public PaymentType PaymentType { get; set; }

    }
    public enum PaymentType
    {
        card,
        cash,
    }
}
