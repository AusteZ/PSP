﻿using PSP.Models.Entities.RelationalTables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSP.Models.Entities
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public float EuroCost { get; set; }
        public int MinutesLength { get; set; }
        public IList<ServiceSlot> ServiceSlots { get; set; } = new List<ServiceSlot>();
        public IList<ServiceDiscount> Discounts { get; set; } = new List<ServiceDiscount>();
    }
}
