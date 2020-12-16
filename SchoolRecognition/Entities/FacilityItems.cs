using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class FacilityItems
    {
        public FacilityItems()
        {
            FacilitySettings = new HashSet<FacilitySettings>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsSummary { get; set; }

        public virtual ICollection<FacilitySettings> FacilitySettings { get; set; }
    }
}
