namespace PSP.Models.RequestBodies
{
    public class CancellationCreate
    {
        public int ServiceSlotId { get; set; }
        public DateTime CancellationTime { get; set; }
        public int CustomerId { get; set; }
    }
}
