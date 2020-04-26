﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class CreateSchoolsDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "A School name is required")]
        [MaxLength(50)]
        public string Name { get; set; }

        


        public Guid? CategoryId { get; set; }
        public Guid? OfficeId { get; set; }
        public Guid? LgId { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public long? YearEstablished { get; set; }
    }
}
