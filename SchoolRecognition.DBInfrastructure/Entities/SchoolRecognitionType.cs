using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SchoolRecognitionType
    {
        public SchoolRecognitionType()
        {
            PaymentPinSettings = new HashSet<PaymentPinSetting>();
        }

        public int Id { get; set; }
        public string TypeOfRecognition { get; set; }
        public string RecognitionCode { get; set; }
        public string AccountCode { get; set; }
        public bool? IsActive { get; set; }
        public bool HasPayment { get; set; }
        public bool IsSchoolCreation { get; set; }

        public virtual ICollection<PaymentPinSetting> PaymentPinSettings { get; set; }
    }
}
