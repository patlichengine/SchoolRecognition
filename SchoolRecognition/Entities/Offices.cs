using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class Offices
    {
        public Offices()
        {
            Schools = new HashSet<Schools>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid? StateId { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }
        public byte[] OfficeImage { get; set; }
        public double? Longitute { get; set; }
        public double? Latitude { get; set; }
        public Guid? OfficeTypeId { get; set; }

        public virtual OfficeTypes OfficeType { get; set; }
        public virtual ICollection<Schools> Schools { get; set; }
    }
}
