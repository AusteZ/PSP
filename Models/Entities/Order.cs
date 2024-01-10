﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Azure;
using PSP.Models.Entities.RelationalTables;

namespace PSP.Models.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<OrderProduct> Products { get; set; } = new List<OrderProduct>();
        public IList<ServiceSlot> ServiceSlots { get; set; } = new List<ServiceSlot>();
    }
}