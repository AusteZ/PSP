using PSP.Models.Entities.RelationalTables;
using PSP.Models.Entities;

namespace PSP.Models.DTOs
{
    public class DiscountCreate
    {
        public int Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<int> ProductIds { get; set; }
        public IList<int> ServiceIds { get; set; }
    }
}
