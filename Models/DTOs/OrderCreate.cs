using PSP.Models.DTOs;

namespace PSP.Models.DTOs
{
    public class OrderCreate
    {
        public int CustomerId { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<int> ProductsIds { get; set; }
        public IList<int> serviceSlotIds { get; set; }
    }
}