using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PSP.Models.Entities.RelationalTables;

namespace PSP.Models.Entities
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Percentage { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }
        public IList<ProductDiscount> Products { get; set; } = new List<ProductDiscount>();
        public IList<ServiceDiscount> Services{ get; set; } = new List<ServiceDiscount>();
    }
}
