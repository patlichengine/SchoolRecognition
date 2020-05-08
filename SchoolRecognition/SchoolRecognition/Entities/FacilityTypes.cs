using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class FacilityTypes
    {
        public FacilityTypes()
        {
            FacilitySettings = new HashSet<FacilitySettings>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool? IsActive { get; set; }
        public bool HasSubject { get; set; }
        public bool HasSpecification { get; set; }
        public bool HasQuantity { get; set; }

        public virtual ICollection<FacilitySettings> FacilitySettings { get; set; }
    }
}
