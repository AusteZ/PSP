namespace PSP.Models.DTOs
{
    public class CouponCreate
    {
        public float EuroPrice { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Used { get; set; }
    }
}
