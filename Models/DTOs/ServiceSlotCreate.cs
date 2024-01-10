namespace PSP.Models.DTOs
{
    public class ServiceSlotCreate
    {
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Time { get; set; }
        public int? PartySize { get; set; }
        public bool Completed { get; set; }
    }
}
