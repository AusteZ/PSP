using PSP.Models.DTOs.Output;

namespace PSP.Models.DTOs
{
    public class ProductOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float PriceEuros { get; set; }
        public string ProductDescription { get; set; }
        public IList<OrderWithNoRelations> Orders { get; set; }
        public IList<DiscountWithNoRelations> Discounts { get; set; }
    }

    public class ProductWithNoRelations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float PriceEuros { get; set; }
        public string ProductDescription { get; set; }
    }

    public class ProductWithQuantity
    {
        public ProductWithNoRelations Product { get; set; }
        public int Quantity { get; set; }
    }
}
