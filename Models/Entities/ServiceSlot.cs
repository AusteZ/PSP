﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSP.Models.Entities
{
    public class ServiceSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int EmployeeId { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public int? PartySize { get; set; }
        public bool Completed { get; set; }
    }
}
