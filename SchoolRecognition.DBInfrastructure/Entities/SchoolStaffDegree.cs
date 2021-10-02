using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SchoolStaffDegree
    {
        public Guid Id { get; set; }
        public Guid? SchoolStaffProfileId { get; set; }
        public Guid? DegreeId { get; set; }
        public Guid? ApprovedCourseId { get; set; }
        public string CourseTitle { get; set; }
        public string DocumentPath { get; set; }
        public byte[] FileContent { get; set; }
        public long? FileSize { get; set; }
        public string FileExtension { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ApprovedCours ApprovedCourse { get; set; }
        public virtual ApplicationUser CreatedByNavigation { get; set; }
        public virtual SchoolStaffProfile SchoolStaffProfile { get; set; }
    }
}
