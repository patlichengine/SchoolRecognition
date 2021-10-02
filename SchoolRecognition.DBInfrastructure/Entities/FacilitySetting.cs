using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class FacilitySetting
    {
        public FacilitySetting()
        {
            SchoolFacilities = new HashSet<SchoolFacility>();
        }

        public Guid Id { get; set; }
        public int? Position { get; set; }
        public Guid? FacilityTypeId { get; set; }
        public string SubjectCode { get; set; }
        public string Specification { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? FacilityItemSettingsId { get; set; }
        public bool IsSpecification { get; set; }
        public bool IsQuantity { get; set; }
        public bool IsBool { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsGeneral { get; set; }
        public bool IsValueRequired { get; set; }

        public virtual ApplicationUser CreatedByNavigation { get; set; }
        public virtual FacilityItemSetting FacilityItemSettings { get; set; }
        public virtual FacilityType FacilityType { get; set; }
        public virtual Subject SubjectCodeNavigation { get; set; }
        public virtual ICollection<SchoolFacility> SchoolFacilities { get; set; }
    }
}
