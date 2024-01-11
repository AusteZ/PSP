using PSP.Models.DTOs;
using PSP.Models.Entities;

namespace PSP.Models.DTOs
{
    public class OrderCreate
    {
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Tips { get; set; }
        public PaymentStatus Status { get; set; }
        public IList<int> ProductsIds { get; set; }
        public IList<int> serviceSlotIds { get; set; }
        public int LoyaltyPointsToUse { get; set; }
    }
}