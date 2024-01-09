using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PSP.Models.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public float Price { get; set; }

        public string? ProductDescription { get; set; }
        [JsonIgnore]
        public List<Order>? Orders { get; set; }
        [JsonIgnore]
        public List<OrderProducts>? OrderProducts { get; set; }
    }
}
