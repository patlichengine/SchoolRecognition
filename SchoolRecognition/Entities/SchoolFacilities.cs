using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolFacilities
    {
        public SchoolFacilities()
        {
            SchoolDeficiencies = new HashSet<SchoolDeficiencies>();
        }

        public Guid Id { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? FacilitySettingId { get; set; }
        public string ValueAupplied { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ApplicationUsers CreatedByNavigation { get; set; }
        public virtual FacilitySettings FacilitySetting { get; set; }
        public virtual Schools School { get; set; }
        public virtual ICollection<SchoolDeficiencies> SchoolDeficiencies { get; set; }
    }
}
