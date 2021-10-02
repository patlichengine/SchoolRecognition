using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class Subject
    {
        public Subject()
        {
            FacilitySettings = new HashSet<FacilitySetting>();
        }

        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string LongCode { get; set; }

        public virtual ICollection<FacilitySetting> FacilitySettings { get; set; }
    }
}
