using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class Pins
    {
        public Pins()
        {
            PinHistories = new HashSet<PinHistories>();
            SchoolPayments = new HashSet<SchoolPayments>();
        }

        public Guid Id { get; set; }
        public Guid? RecognitionTypeId { get; set; }
        public string SerialPin { get; set; }
        public bool IsActive { get; set; }
        public bool IsInUse { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual RecognitionTypes RecognitionType { get; set; }
        public virtual ICollection<PinHistories> PinHistories { get; set; }
        public virtual ICollection<SchoolPayments> SchoolPayments { get; set; }
    }
}
