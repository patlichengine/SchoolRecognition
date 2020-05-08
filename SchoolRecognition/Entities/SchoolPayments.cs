using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolPayments
    {
        public Guid Id { get; set; }
        public Guid? PinId { get; set; }
        public Guid? SchoolId { get; set; }
        public decimal? Amount { get; set; }
        public string ReceiptNo { get; set; }
        public byte[] ReceiptImage { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Pins Pin { get; set; }
        public virtual Schools School { get; set; }
    }
}
