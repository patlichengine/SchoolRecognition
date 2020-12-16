using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class FacilityTypesDto
    {
        public Guid Id { get; set; }

        [DisplayName("Facility Name")]
        public string Title { get; set; }

        [DisplayName("Set Facility Active Status")]
        [UIHint("IsActive")]
        public bool IsActive { get; set; }
        
        [DisplayName("Set Facility Global Status")]
        [UIHint("IsGlobal")]
        public bool IsGlobal { get; set; }

    }
}
