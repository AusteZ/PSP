using PSP.Models.Entities;
using System.Text.Json.Serialization;

namespace PSP.Models.DTOs
{
    public class OrderServicesCreate
    {
        [JsonIgnore]
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        public int ServiceId { get; set; }
        [JsonIgnore]
        public Service? Service { get; set; }
    }
}
