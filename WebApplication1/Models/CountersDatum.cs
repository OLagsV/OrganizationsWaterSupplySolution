using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizationsWaterSupplyL4.Models
{
    public partial class CountersDatum
    {
        [Key]
        public int CountersDataId { get; set; }
        public int? CounterRegistrationNumber { get; set; }
        public DateTime? DataCheckDate { get; set; }
        public int? Volume { get; set; }

        public virtual Counter? RegistrationNumber { get; set; }
    }
}
