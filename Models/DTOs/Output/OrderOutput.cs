using PSP.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PSP.Models.DTOs.Output
{
    public class OrderOutput
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Tips { get; set; }
        public IList<ServiceSlotWithServiceOutput> ServiceSlots { get; set; }
        public IList<ProductWithDiscount> Products { get; set; }
    }

    public class OrderWithNoRelations
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Tips { get; set; }
    }
}
