using SchoolRecognition.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class SchoolClassesDto
    {
        public Guid Id { get; set; }



        public Guid ClassId { get; set; }
        public Guid SchoolId { get; set; }
        public byte NoOfStreams { get; set; }
        public int TotalStudents { get; set; }

        public virtual ClassSettings Class { get; set; }
        public virtual Schools School { get; set; }


        //[JsonIgnore]
        //public  IEnumerable<SchoolClassAllocations> SchoolClassAllocations { get; set; }
        //public  IEnumerable<SchoolStaffSubjects> SchoolStaffSubjects { get; set; }

    }
}
