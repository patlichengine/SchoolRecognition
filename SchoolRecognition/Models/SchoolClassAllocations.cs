using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class SchoolClassAllocations
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public Guid SchoolId { get; set; }
        public byte NoOfArms { get; set; }
        public int TotalStudents { get; set; }

        public virtual SchoolClasses Class { get; set; }
        public virtual Schools School { get; set; }
    }
}
