using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class CentreSubjectSanctions
    {
        public Guid Id { get; set; }
        public Guid? CentreSanctionId { get; set; }
        public Guid? SubjectId { get; set; }
        public bool? IsActive { get; set; }

        public virtual CentreSanctions CentreSanction { get; set; }
        public virtual Subjects Subject { get; set; }
    }
}
