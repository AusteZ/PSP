using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PSP.Models.Entities
{
    public class OrderServices
    {

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        public Order? Order { get; set; }

        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }
        public Service? Service { get; set; }

    }
}
