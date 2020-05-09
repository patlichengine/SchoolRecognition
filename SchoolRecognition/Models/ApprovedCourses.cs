using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class ApprovedCourses
    {
        public ApprovedCourses()
        {
            SchoolStaffDegrees = new HashSet<SchoolStaffDegrees>();
        }

        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<SchoolStaffDegrees> SchoolStaffDegrees { get; set; }
    }
}
