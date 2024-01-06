using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSP.Models
{
    public class ServiceSlotBooking
    {
        public int CustomerId { get; set; }
        public int PartySize { get; set; }
    }
}
