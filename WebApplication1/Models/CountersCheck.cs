using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizationsWaterSupplyL4.Models
{
    public partial class CountersCheck
    {
        [Key]
        public int CountersCheckId { get; set; }
        public int? CounterRegistrationNumber { get; set; }
        public DateTime? CheckDate { get; set; }
        public string? CheckResult { get; set; }

        public virtual Counter? RegistrationNumber { get; set; }
    }
}
