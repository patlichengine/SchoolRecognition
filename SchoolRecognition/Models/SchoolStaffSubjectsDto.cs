using System;

namespace SchoolRecognition.Models
{
    public class SchoolStaffSubjectsDto
    {
        public Guid Id { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? SubjectId { get; set; }
        public Guid? ClassId { get; set; }
        public DateTime DateCreated { get; set; }

        
        public string SchoolClass  { get; set; }
        public string SchoolStaffProfiles { get; set; }
        public string SchoolSubjects  { get; set; }
    }
}