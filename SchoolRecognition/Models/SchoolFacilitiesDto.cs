using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class SchoolFacilitiesViewDto
    {
        public Guid Id { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? FacilitySettingId { get; set; }
        public string ValueAupplied { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public string CreatedByUser{ get; set; }
        //FacilitySettings
        public int? FacilitySettingPosition { get; set; }
        public Guid? SubjectId { get; set; }
        public string FacilitySettingSpecification { get; set; }
        public int? FacilitySettingQuantity { get; set; }
        //FacilityItemSettings
        public string FacilityItemSettingsDescription { get; set; }
        //School
        public String SchoolName { get; set; }
        public String SchoolAddress { get; set; }
        public String SchoolEmailAddress { get; set; }
        public String SchoolPhoneNo { get; set; }
        public long? YearEstablished { get; set; }
        public String SchoolCategoryName { get; set; }
    }
}
