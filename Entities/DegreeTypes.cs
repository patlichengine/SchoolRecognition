using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class DegreeTypes
    {
        public DegreeTypes()
        {
            SchoolStaffDegrees = new HashSet<SchoolStaffDegrees>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<SchoolStaffDegrees> SchoolStaffDegrees { get; set; }
    }
}
