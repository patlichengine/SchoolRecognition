using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolClasses
    {
        public SchoolClasses()
        {
            SchoolClassAllocations = new HashSet<SchoolClassAllocations>();
            SchoolStaffSubjects = new HashSet<SchoolStaffSubjects>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SchoolClassAllocations> SchoolClassAllocations { get; set; }
        public virtual ICollection<SchoolStaffSubjects> SchoolStaffSubjects { get; set; }
    }
}
