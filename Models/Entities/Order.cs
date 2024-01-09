using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PSP.Models.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<OrderProducts>? OrderProducts { get; set; }

        public virtual ICollection<OrderServices>? OrderServices { get; set; }
    }
}
