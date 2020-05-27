using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class SchoolsDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "A School name is required")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "A School Unique Code is Required")]
        [MaxLength(2)]
     
       
        public Guid? CategoryId { get; set; }
        public Guid? OfficeId { get; set; }
        public Guid? LgId { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public long? YearEstablished { get; set; }

        public string CategoryName { get; set; }
        public string OfficeName { get; set; }
        public string LgName { get; set; }


    }


    public class SchoolsViewDto
    {
        public Guid Id { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCategoryName { get; set; }
        public string OfficeName { get; set; }
        public string LgaName { get; set; }
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
