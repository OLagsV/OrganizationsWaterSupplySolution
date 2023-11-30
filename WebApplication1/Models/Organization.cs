using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationsWaterSupplyL4.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Counters = new HashSet<Counter>();
            RateOrgNotes = new HashSet<RateOrgNote>();
        }

        public int OrganizationId { get; set; }
        public string? OrgName { get; set; }
        public string? OwnershipType { get; set; }
        public string? Adress { get; set; }
        public string? DirectorFullname { get; set; }
        public string? DirectorPhone { get; set; }
        public string? ResponsibleFullname { get; set; }
        public string? ResponsiblePhone { get; set; }

        public virtual ICollection<Counter> Counters { get; set; }
        [NotMapped]
        public virtual ICollection<RateOrgNote> RateOrgNotes { get; set; }
    }
}
