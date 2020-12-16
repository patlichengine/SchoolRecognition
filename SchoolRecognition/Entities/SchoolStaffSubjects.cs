using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolStaffSubjects
    {
        public Guid Id { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? SubjectId { get; set; }
        public Guid? ClassId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ClassSettings Class { get; set; }
        public virtual SchoolStaffProfiles Staff { get; set; }
        public virtual Subjects Subject { get; set; }
    }
}
