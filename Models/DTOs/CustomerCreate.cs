using System.ComponentModel.DataAnnotations;

namespace PSP.Models.DTOs
{
    public class CustomerCreate
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
        public int LoyaltyPoints { get; set; }
    }
}
