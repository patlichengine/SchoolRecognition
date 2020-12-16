using SchoolRecognition.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class ClassSettingsUpdateDto
    {



        public string Name { get; set; }


        //[JsonIgnore]
        //public  IEnumerable<SchoolClassAllocations> SchoolClassAllocations { get; set; }
        //public  IEnumerable<SchoolStaffSubjects> SchoolStaffSubjects { get; set; }

    }
}
