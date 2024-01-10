namespace PSP.Models.DTOs
{
    public class ServiceSlotOutput
    {
        public int Id { get; set; }
        public ServiceOfServiceSlotOutput Service { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Time { get; set; }
        public int? CustomerId { get; set; }
        public int? PartySize { get; set; }
        public bool Completed { get; set; }
    }

    public class ServiceOfServiceSlotOutput
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public float EuroCost { get; set; }
        public int MinutesLength { get; set; }
    }
}
