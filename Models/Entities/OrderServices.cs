using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PSP.Models.Entities
{
    public class OrderServices
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
