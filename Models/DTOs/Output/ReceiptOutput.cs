﻿namespace PSP.Models.DTOs.Output
{
    public class ReceiptOutput
    {
        public OrderOutput Order { get; set; }
        public decimal Total { get; set; }
        public decimal Tip { get; set; }
        public DateTime? Date { get; set; }
        public string PaymentType { get; set; }
    }
}