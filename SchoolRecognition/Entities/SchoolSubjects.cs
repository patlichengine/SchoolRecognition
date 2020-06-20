using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolSubjects
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid SubjectId { get; set; }
        public bool IsRecognised { get; set; }
        public bool IsSuspended { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string Remarks { get; set; }

        public virtual ApplicationUsers CreatedByNavigation { get; set; }
        public virtual ApplicationUsers ModifiedByNavigation { get; set; }
        public virtual Schools School { get; set; }
        public virtual Subjects Subject { get; set; }
    }
}
