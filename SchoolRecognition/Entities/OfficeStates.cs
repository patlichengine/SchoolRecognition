using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class OfficeStates
    {
        public Guid Id { get; set; }
        public Guid? StateId { get; set; }
        public Guid? OfficeId { get; set; }

        public virtual Offices Office { get; set; }
        public virtual States State { get; set; }
    }
}
