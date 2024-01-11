namespace PSP.Models.Entities
{
    public class Receipt
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime? Date { get; set; }
        public decimal Total { get; set; }
        public int? CouponId { get; set; }
        public Coupon? Coupon { get; set; }
        public PaymentType PaymentType { get; set; }
    }

    public enum PaymentType
    {
        card,
        cash,
    }
}