using PSP.Models.DTOs.Output;

namespace PSP.Models.DTOs
{
    public class ServiceSlotOutput
    {
        public int Id { get; set; }
        public ServiceWithNoRelations Service { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Time { get; set; }
        public bool Completed { get; set; }
        public int? OrderId { get; set; }
        public OrderWithNoRelations? Order { get; set; }
    }

    public class ServiceSlotWithNoRelations
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Time { get; set; }
        public int? CustomerId { get; set; }
        public int? PartySize { get; set; }
        public bool Completed { get; set; }
    }

    public class ServiceSlotWithServiceOutput
    {
        public int Id { get; set; }
        public ServiceWithNoRelations Service { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Time { get; set; }
        public bool Completed { get; set; }
        public int? OrderId { get; set; }
    }
}
