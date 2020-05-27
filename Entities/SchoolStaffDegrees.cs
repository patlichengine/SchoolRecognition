using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolStaffDegrees
    {
        public Guid Id { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? DegreeId { get; set; }
        public Guid? DegreeTypeId { get; set; }
        public Guid? CourseId { get; set; }
        public string CredentialPath { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ApprovedCourses Course { get; set; }
        public virtual ApplicationUsers CreatedByNavigation { get; set; }
        public virtual Degrees Degree { get; set; }
        public virtual DegreeTypes DegreeType { get; set; }
        public virtual SchoolStaffProfiles Staff { get; set; }
    }
}
