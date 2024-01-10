using PSP.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PSP.Models.DTOs
{
    public class OrderOutput
    {
        public int Id { get; set; }
        public int CustomerId { get; set; } 
        public Customer Customer { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<ServiceSlotOutput> ServiceSlots { get; set; }
        public IList<ProductOfOrder> Products { get; set; }

    }

    public class ProductOfOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float PriceEuros { get; set; }
        public string ProductDescription { get; set; }
    }
}
