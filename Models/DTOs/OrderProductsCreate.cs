using PSP.Models.Entities;
using System.Text.Json.Serialization;

namespace PSP.Models.DTOs
{
    public class OrderProductsCreate
    {
        [JsonIgnore]
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
