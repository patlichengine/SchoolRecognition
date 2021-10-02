using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SchoolPayment
    {
        public Guid Id { get; set; }
        public Guid? PinId { get; set; }
        public Guid? SchoolProfileId { get; set; }
        public decimal? Amount { get; set; }
        public string ReceiptNo { get; set; }
        public byte[] ReceiptImage { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }

        public virtual ApplicationUser CreatedByNavigation { get; set; }
        public virtual PaymentPin Pin { get; set; }
        public virtual SchoolProfile SchoolProfile { get; set; }
    }
}
