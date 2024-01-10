namespace PSP.Models.Entities
{
    public class OrderService
    {
        public int OrderId { get; set; }
        public int ServiceSlotId { get; set; }
        public Order Order { get; set; }
        public ServiceSlot ServiceSlot { get; set; }

    }
}