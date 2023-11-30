using System;
using System.Collections.Generic;

namespace OrganizationsWaterSupplyL4.Models
{
    public partial class RateOrgNote
    {
        public int RateOrgNoteId { get; set; }
        public int? RateId { get; set; }
        public int? OrganizationId { get; set; }

        public virtual Organization? Organization { get; set; }
        public virtual Rate? Rate { get; set; }
    }
}
