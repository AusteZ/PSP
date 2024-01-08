using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSP.Models.Entities
{
    public class ServiceSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Time { get; set; }
        public int? CustomerId { get; set; }
        public int? PartySize { get; set; }
        public bool Completed { get; set; }
    }
}
