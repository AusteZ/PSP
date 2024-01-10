namespace PSP.Models.Entities.RelationalTables
{
    public class ServiceDiscount
    {
        public int ServiceId { get; set; }
        public int DiscountId { get; set; }
        public Service Service { get; set; }
        public Discount Discount { get; set; }
    }
}
