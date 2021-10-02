using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class ApplicationUserState
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string StateCode { get; set; }

        public virtual State StateCodeNavigation { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
