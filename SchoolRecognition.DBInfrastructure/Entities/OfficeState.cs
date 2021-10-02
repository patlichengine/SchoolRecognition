using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class OfficeState
    {
        public Guid Id { get; set; }
        public Guid? OfficeId { get; set; }
        public string StateCode { get; set; }
        public DateTime? DateCaptured { get; set; }
        public Guid? ModifiedBy { get; set; }

        public virtual Office Office { get; set; }
        public virtual State StateCodeNavigation { get; set; }
    }
}
