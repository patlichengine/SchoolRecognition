using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class PinHistories
    {
        public Guid Id { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? PinId { get; set; }
        public DateTime? DateActive { get; set; }
        public Guid? CreatedBy { get; set; }

        public virtual Pins Pin { get; set; }
        public virtual Schools School { get; set; }
    }
}
