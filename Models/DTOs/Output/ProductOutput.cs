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
    }

    public class ProductWithNoRelations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float PriceEuros { get; set; }
        public string ProductDescription { get; set; }
    }
}
