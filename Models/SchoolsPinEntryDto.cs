using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class SchoolsPinEntryDto
    {
        [Display(Name = "Serial Pin")]
        public string SerialPin { get; set; }

       
        public Guid SchoolId { get; set; }
        //public Guid? RecognitionTypeId { get; set; }
    }
}
