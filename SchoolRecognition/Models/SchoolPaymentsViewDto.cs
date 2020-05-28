using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class SchoolPaymentsViewDto
    {
        public Guid Id { get; set; }
        public decimal? AmountPaid { get; set; }
        public string PaymentReceiptNo { get; set; }
        public byte[] PaymentReceiptImage { get; set; }
        public DateTime? DateCreated { get; set; }

        //CreatedByNavigation
        public String CreatedByUser { get; set; }
        //School
        public String SchoolName { get; set; }
        public String SchoolCategoryName { get; set; }
        //Pin
        public String PinSerialNumber { get; set; }
        public String RecognitionTypeName { get; set; }
        public String RecognitionTypeCode{ get; set; }
        public Guid? PinId { get; internal set; }
        public Guid? SchoolId { get; internal set; }
        public Guid? CreatedBy { get; internal set; }
    }
    public class SchoolPaymentsCreateDto
    {
        public Guid Id { get; set; }
        public decimal? AmountPaid { get; set; }
        public string PaymentReceiptNo { get; set; }
        public byte[] PaymentReceiptImage { get; set; }
        public DateTime? DateCreated { get; set; }

        //CreatedByNavigation
        public String CreatedByUser { get; set; }
        //School
        public String SchoolName { get; set; }
        public String SchoolCategoryName { get; set; }
        //Pin
        public String PinSerialNumber { get; set; }
        public Guid RecognitionTypeId { get; set; }
    }
}
