﻿namespace PSP.Models.DTOs
{
    public class CustomerCreate
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int LoyaltyPoints { get; set; }
    }
}
