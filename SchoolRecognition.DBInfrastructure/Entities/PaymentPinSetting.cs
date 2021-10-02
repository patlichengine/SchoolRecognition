using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class PaymentPinSetting
    {
        public PaymentPinSetting()
        {
            PaymentPins = new HashSet<PaymentPin>();
        }

        public Guid Id { get; set; }
        public bool IsActve { get; set; }
        public string Description { get; set; }
        public double? Amount { get; set; }
        public int? SchoolRecognitionType { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }

        public virtual ApplicationUser CreatedByNavigation { get; set; }
        public virtual SchoolRecognitionType SchoolRecognitionTypeNavigation { get; set; }
        public virtual ICollection<PaymentPin> PaymentPins { get; set; }
    }
}
