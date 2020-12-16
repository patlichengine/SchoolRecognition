using SchoolRecognition.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class SchoolClassesDependencyDto
    {
        public virtual IEnumerable<SchoolClassAllocations> SchoolClassAllocations { get; set; }
        public virtual IEnumerable<SchoolStaffSubjects> SchoolStaffSubjects { get; set; }
    }
}
