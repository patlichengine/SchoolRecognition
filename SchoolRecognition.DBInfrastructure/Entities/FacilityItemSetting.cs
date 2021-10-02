using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class FacilityItemSetting
    {
        public FacilityItemSetting()
        {
            FacilitySettings = new HashSet<FacilitySetting>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<FacilitySetting> FacilitySettings { get; set; }
    }
}
