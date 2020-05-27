using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class ApplicationUserStates
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? StateId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ApplicationUsers CreatedByNavigation { get; set; }
        public virtual States State { get; set; }
        public virtual ApplicationUsers User { get; set; }
    }
}
