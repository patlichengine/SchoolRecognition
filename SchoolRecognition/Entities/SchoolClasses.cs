using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolClasses
    {
        //public SchoolClasses()
        //{
        //    SchoolClassAllocations = new HashSet<SchoolClassAllocations>();
        //    SchoolStaffSubjects = new HashSet<SchoolStaffSubjects>();
        //}
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public Guid SchoolId { get; set; }
        public byte NoOfStreams { get; set; }
        public int TotalStudents { get; set; }

        public virtual ClassSettings Class { get; set; }
        public virtual Schools School { get; set; }
     

        //public virtual ICollection<SchoolClassAllocations> SchoolClassAllocations { get; set; }
        //public virtual ICollection<SchoolStaffSubjects> SchoolStaffSubjects { get; set; }
    }
}
