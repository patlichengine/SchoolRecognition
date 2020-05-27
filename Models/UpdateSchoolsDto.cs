using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class UpdateSchoolsDto 
    {
        public long? YearEstablished { get; set; }

        [Required(ErrorMessage = "A School name is required")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The School Address must be specified and should not be more that 50 characters")]
        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string EmailAddress { get; set; }

        [MaxLength(20)]
        // [RegularExpression(@"^\([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public virtual string PhoneNo { get; set; }

    }
}
