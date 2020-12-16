using SchoolRecognition.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class FacilitySettingsDto
    {

        public Guid Id { get; set; }
        public Guid? FacilityTypeId { get; set; }
        public Guid? FacilityItemId { get; set; }
        public int ItemNo { get; set; }
        public Guid? SubjectId { get; set; }
        public string Specification { get; set; }
        public int Quantity { get; set; }
        public Guid? CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateCreated { get; set; }

        public ApplicationUsers CreatedByNavigation { get; set; }
        public  FacilityTypes FacilityType { get; set; }
        public   FacilityItems FacilityItem { get; set; }
        public Subjects Subject { get; set; }
        public  IEnumerable<SchoolFacilities> SchoolFacilities { get; set; }
    }
}
