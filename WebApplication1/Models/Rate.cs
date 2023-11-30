using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationsWaterSupplyL4.Models
{
    public partial class Rate
    {
        public int RateId { get; set; }
        public string? RateName { get; set; }
        public decimal? Price { get; set; }
        [NotMapped]
        public virtual RateOrgNote? RateOrgNote { get; set; }
    }
}
