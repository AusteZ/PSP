using PSP.Models.Entities;

namespace PSP.Models.DTOs
{
    public class OrderCreate
    {
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<OrderProductsCreate>? OrderProducts { get; set; }
        public ICollection<OrderServicesCreate>? OrderServices { get; set; }

    }
}
