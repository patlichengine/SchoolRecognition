using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class PinHistories
    {
        public Guid Id { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? PinId { get; set; }
        public DateTime? DateActive { get; set; }
        public Guid? CreatedBy { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Pins Pin { get; set; }
        public virtual Schools School { get; set; }
    }
}
