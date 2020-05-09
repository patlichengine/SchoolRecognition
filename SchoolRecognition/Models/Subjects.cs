using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class Subjects
    {
        public Subjects()
        {
            FacilitySettings = new HashSet<FacilitySettings>();
            SchoolStaffSubjects = new HashSet<SchoolStaffSubjects>();
        }

        public Guid Id { get; set; }
        public string SubjectCode { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public bool HasItem { get; set; }
        public bool IsTradeSubject { get; set; }
        public bool IsCoreSubject { get; set; }

        public virtual ICollection<FacilitySettings> FacilitySettings { get; set; }
        public virtual ICollection<SchoolStaffSubjects> SchoolStaffSubjects { get; set; }
    }
}
