using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class SchoolsAssessmentDto
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid FacilityItemId { get; set; }
        public Guid CreatedBy { get; set; }

        
    }
}
