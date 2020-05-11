using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class States
    {
        public States()
        {
            ApplicationUserStates = new HashSet<ApplicationUserStates>();
            LocalGovernments = new HashSet<LocalGovernments>();
            OfficeStates = new HashSet<OfficeStates>();
            Offices = new HashSet<Offices>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<ApplicationUserStates> ApplicationUserStates { get; set; }
        public virtual ICollection<LocalGovernments> LocalGovernments { get; set; }
        public virtual ICollection<OfficeStates> OfficeStates { get; set; }
        public virtual ICollection<Offices> Offices { get; set; }
    }
}
