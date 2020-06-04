using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class FacilityItemSettings
    {
        public FacilityItemSettings()
        {
            FacilitySettings = new HashSet<FacilitySettings>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<FacilitySettings> FacilitySettings { get; set; }
    }
}
