using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class OfficeTypes
    {
        public OfficeTypes()
        {
            Offices = new HashSet<Offices>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Offices> Offices { get; set; }
    }
}
