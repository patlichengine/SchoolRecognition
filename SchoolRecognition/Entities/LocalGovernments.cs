using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class LocalGovernments
    {
        public LocalGovernments()
        {
            OfficeLocalGovernments = new HashSet<OfficeLocalGovernments>();
            Schools = new HashSet<Schools>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? StateId { get; set; }

        public virtual States State { get; set; }
        public virtual ICollection<OfficeLocalGovernments> OfficeLocalGovernments { get; set; }
        public virtual ICollection<Schools> Schools { get; set; }
    }
}
