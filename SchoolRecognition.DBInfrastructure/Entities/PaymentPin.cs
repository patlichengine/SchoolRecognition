using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class PaymentPin
    {
        public PaymentPin()
        {
            PinHistories = new HashSet<PinHistory>();
            SchoolPayments = new HashSet<SchoolPayment>();
        }

        public Guid Id { get; set; }
        public Guid? PaymentPinSettingId { get; set; }
        public string SerialPin { get; set; }
        public bool IsActive { get; set; }
        public bool IsInUse { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ApplicationUser CreatedByNavigation { get; set; }
        public virtual PaymentPinSetting PaymentPinSetting { get; set; }
        public virtual ICollection<PinHistory> PinHistories { get; set; }
        public virtual ICollection<SchoolPayment> SchoolPayments { get; set; }
    }
}
