using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class CreateFacilitySettingsDto
    {

        public Guid Id { get; set; }
        public Guid FacilityTypeId { get; set; }
        public Guid FacilityItemSettingsId { get; set; }
        public string Description { get; set; }
        public int? Position { get; set; }
        public Guid SubjectId { get; set; }
        public string Specification { get; set; }
        public int? Quantity { get; set; }
        public Guid? CreatedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateCreated { get; set; }
    }
}
