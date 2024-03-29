﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PSP.Models.DTOs.Output;

namespace PSP.Models.DTOs
{
    public class ServiceOutput
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public float EuroCost { get; set; }
        public int MinutesLength { get; set; }
        public IList<ServiceSlotWithNoRelations> ServiceSlots { get; set; }
        public IList<DiscountWithNoRelations> Discounts { get; set; }
    }

    public class ServiceWithNoRelations
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public float EuroCost { get; set; }
        public int MinutesLength { get; set; }
    }
}
