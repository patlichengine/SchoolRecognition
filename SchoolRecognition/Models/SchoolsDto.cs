using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{

    public class SchoolsViewDto
    {
        public Guid Id { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCategoryName { get; set; }
        public string SchoolCategoryCode { get; set; }
        public string OfficeName { get; set; }
        public string LgaName { get; set; }
        public string LgaCode { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public long? YearEstablished { get; set; }
        public bool IsRecognised { get; set; }
        public bool IsVetted { get; set; }
        public bool IsInspected { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsRecommended { get; set; }
        public bool HasDeficientSubject { get; set; }
        public bool HasDeficientFacilitiy { get; set; }
        //
        public Guid? CategoryId { get; set; }
        public Guid? OfficeId { get; set; }
        public Guid? LgId { get; set; }

    }
    public class SchoolsCreateDto
    {
        public Guid Id { get; set; }
        public string SchoolName { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? OfficeId { get; set; }
        public Guid? LgId { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public long? YearEstablished { get; set; }
        public bool IsRecognised { get; set; }
        public bool IsVetted { get; set; }
        public bool IsInspected { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsRecommended { get; set; }
        public bool HasDeficientSubject { get; set; }
        public bool HasDeficientFacilitiy { get; set; }

    }


}
