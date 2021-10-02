using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class PinHistory
    {
        public Guid Id { get; set; }
        public Guid? PinId { get; set; }
        public DateTime? DateActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? SchoolProfileId { get; set; }

        public virtual ApplicationUser CreatedByNavigation { get; set; }
        public virtual PaymentPin Pin { get; set; }
        public virtual SchoolProfile SchoolProfile { get; set; }
    }
}
