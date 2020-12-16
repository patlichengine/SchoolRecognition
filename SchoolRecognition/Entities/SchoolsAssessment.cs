using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolsAssessment
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid FacilityItemId { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual ApplicationUsers CreatedByNavigation { get; set; }
        public virtual FacilityItems FacilityItem { get; set; }
        public virtual Schools School { get; set; }
    }
}
