﻿namespace PSP.Models.DTOs
{
    public class CardPayment
    {
        public string CardNumber { get; set; }
        public string CVC { get; set; }
        public string CardHolder { get; set; }
        public string ExpiryDate { get; set; }
    }
}
