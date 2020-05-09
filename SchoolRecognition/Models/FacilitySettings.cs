using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class FacilitySettings
    {
        public FacilitySettings()
        {
            SchoolFacilities = new HashSet<SchoolFacilities>();
        }

        public Guid Id { get; set; }
        public Guid FacilityTypeId { get; set; }
        public string Description { get; set; }
        public int? Position { get; set; }
        public Guid? SubjectId { get; set; }
        public string Specification { get; set; }
        public int? Quantity { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual FacilityTypes FacilityType { get; set; }
        public virtual Subjects Subject { get; set; }
        public virtual ICollection<SchoolFacilities> SchoolFacilities { get; set; }
    }
}
