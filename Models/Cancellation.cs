using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PSP.Models
{
    public class Cancellation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ServiceSlotId { get; set; }
        public DateTime CancellationTime { get; set; }
    }
}
