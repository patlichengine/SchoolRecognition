using SchoolRecognition.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class ClassSettingsDto
    {
        [Key]
        public Guid Id { get; set; }



        public string Name { get; set; }

        public IEnumerable<SchoolClassesDto> SchoolClasses { get; set; }
        public IEnumerable<SchoolStaffSubjectsDto> SchoolStaffSubjects { get; set; }
      
        //[JsonIgnore]
        //public  IEnumerable<SchoolClassAllocations> SchoolClassAllocations { get; set; }
        //public  IEnumerable<SchoolStaffSubjects> SchoolStaffSubjects { get; set; }

    }
}
