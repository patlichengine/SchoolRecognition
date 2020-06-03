using Microsoft.AspNetCore.Http;
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
        public String RecognitionTypeCode { get; set; }
        public Guid? PinId { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? CreatedBy { get; set; }
    }

    public class SchoolPaymentsCreateDto
    {
        public Guid Id { get; set; }
        public decimal? AmountPaid { get; set; }
        public string PaymentReceiptNo { get; set; }
        public byte[] PaymentReceiptImage { get; set; }
        public DateTime? DateCreated { get; set; }

        public Guid? PinId { get; set; }
        public Guid? RecognitionTypeId { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? CreatedBy { get; set; }

        #region From Centre

        public string CentreNo { get; set; }

        #endregion

        #region From SchoolsCreateDTO

        public string SchoolName { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? OfficeId { get; set; }
        public Guid? LgId { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public long? YearEstablished { get; set; }
        public IFormFile UploadedFile { get; set; }

        #endregion
    }



    public class SchoolPaymentsCreationDependecyDto
    {
        public IEnumerable<SchoolCategorysViewDto> SchoolCategorys { get; set; }
        //public IEnumerable<RecognitionTypesViewDto> RecognitionTypes { get; set; }
        public IEnumerable<OfficeLocalGovernmentsViewDto> OfficeLocalGovernments { get; set; }
    }
}
