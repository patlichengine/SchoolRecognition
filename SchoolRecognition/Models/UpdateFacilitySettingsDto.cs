using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class UpdateFacilitySettingsDto
    {

       
        public Guid FacilityTypeId { get; set; }
        public string Description { get; set; }
        public int? Position { get; set; }
        public Guid? SubjectId { get; set; }
        public string Specification { get; set; }
        public int? Quantity { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
