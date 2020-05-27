using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class OfficeLocalGovernments
    {
        public Guid Id { get; set; }
        public Guid? LocalGovernmentId { get; set; }
        public Guid? OfficeId { get; set; }

        public virtual LocalGovernments LocalGovernment { get; set; }
        public virtual Offices Office { get; set; }
    }
}
