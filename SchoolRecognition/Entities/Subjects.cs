using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class Subjects
    {
        public Subjects()
        {
            CentreSubjectSanctions = new HashSet<CentreSubjectSanctions>();
            SchoolStaffSubjects = new HashSet<SchoolStaffSubjects>();
            SchoolSubjects = new HashSet<SchoolSubjects>();
        }

        public Guid Id { get; set; }
        public string SubjectCode { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public bool HasItem { get; set; }
        public bool IsTradeSubject { get; set; }
        public bool IsCoreSubject { get; set; }

        public virtual ICollection<CentreSubjectSanctions> CentreSubjectSanctions { get; set; }
        public virtual ICollection<SchoolStaffSubjects> SchoolStaffSubjects { get; set; }
        public virtual ICollection<SchoolSubjects> SchoolSubjects { get; set; }
    }
}
