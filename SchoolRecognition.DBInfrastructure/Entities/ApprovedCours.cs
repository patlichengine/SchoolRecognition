using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class ApprovedCours
    {
        public ApprovedCours()
        {
            SchoolStaffDegrees = new HashSet<SchoolStaffDegree>();
        }

        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<SchoolStaffDegree> SchoolStaffDegrees { get; set; }
    }
}
