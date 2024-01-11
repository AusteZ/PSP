namespace PSP.Models.DTOs.Output
{
    public class DiscountOutput
    {
        public int Id { get; set; }
        public int Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<ProductWithNoRelations> Products { get; set; }
        public IList<ServiceWithNoRelations> Services{ get; set; }
    }

    public class DiscountWithNoRelations
    {
        public int Id { get; set; }
        public int Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
