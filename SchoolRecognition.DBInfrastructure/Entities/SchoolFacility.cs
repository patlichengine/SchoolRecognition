using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SchoolFacility
    {
        public Guid Id { get; set; }
        public Guid? SchoolRecognitionTrailId { get; set; }
        public Guid? FacilitySettingId { get; set; }
        public string ValueApplied { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCurrent { get; set; }

        public virtual ApplicationUser CreatedByNavigation { get; set; }
        public virtual FacilitySetting FacilitySetting { get; set; }
    }
}
