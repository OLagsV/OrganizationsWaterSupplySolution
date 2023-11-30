using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizationsWaterSupplyL4.Models
{
    public partial class CounterModel
    {
        public CounterModel()
        {
            Counters = new HashSet<Counter>();
        }
        [System.ComponentModel.DataAnnotations.Key]
        public int ModelId { get; set; }
        public string? ModelName { get; set; }
        public string? Manufacturer { get; set; }
        public int? ServiceTime { get; set; }

        public virtual ICollection<Counter> Counters { get; set; }
    }
}
