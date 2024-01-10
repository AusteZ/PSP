using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PSP.Models.DTOs
{
    public class ServiceOutput
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public float EuroCost { get; set; }
        public int MinutesLength { get; set; }
        public IList<ServiceSlotOfServiceOutput> ServiceSlots { get; set; }
    }

    public class ServiceSlotOfServiceOutput
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Time { get; set; }
        public int? CustomerId { get; set; }
        public int? PartySize { get; set; }
        public bool Completed { get; set; }
    }
}
