namespace PSP.Models.DTOs
{
    public class OrderPatch
    {
        public IList<int> ServiceSlotsToAdd { get; set; } = new List<int>();
        public IList<int> ServiceSlotsToRemove { get; set; } = new List<int>();
        public IList<int> ProductsToAdd { get; set; } = new List<int>();
        public IList<int> ProductsToRemove { get; set; } = new List<int>();
        public float? Tips { get; set; }
        public int? LoyaltyPointsToUse { get; set; }
    }
}
