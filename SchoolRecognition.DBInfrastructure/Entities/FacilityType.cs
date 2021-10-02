using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class FacilityType
    {
        public FacilityType()
        {
            FacilitySettings = new HashSet<FacilitySetting>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool? IsActive { get; set; }
        public bool IsSubject { get; set; }
        public int? Position { get; set; }

        public virtual ICollection<FacilitySetting> FacilitySettings { get; set; }
    }
}
