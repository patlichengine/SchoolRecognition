using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class OfficeLocalGovernment
    {
        public Guid Id { get; set; }
        public Guid? OfficeId { get; set; }
        public int? LgaId { get; set; }
        public DateTime? DateCaptured { get; set; }
        public Guid? ModifiedBy { get; set; }

        public virtual Lga Lga { get; set; }
    }
}
