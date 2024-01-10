namespace PSP.Models.DTOs
{
    public class CancellationCreate
    {
        public int ServiceSlotId { get; set; }
        public DateTime CancellationTime { get; set; }
        public int OrderId { get; set; }
    }
}
