using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class AuditTrail
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Users User { get; set; }
    }
}
