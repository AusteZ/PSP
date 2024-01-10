using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PSP.Models.Entities.RelationalTables;

namespace PSP.Models.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public float PriceEuros { get; set; }
        public string ProductDescription { get; set; }
        public IList<OrderProduct> Orders { get; set; } = new List<OrderProduct>();
    }
}