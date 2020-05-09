using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class SchoolFacilities
    {
        public Guid Id { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? FacilitySettingId { get; set; }
        public string ValueAupplied { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual FacilitySettings FacilitySetting { get; set; }
        public virtual Schools School { get; set; }
    }
}
