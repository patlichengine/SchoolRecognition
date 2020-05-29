﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class SchoolCategorysCreateDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "A School name is required")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "A School Unique Code is Required")]
        [MaxLength(2)]
        public string Code { get; set; }
    }

    public class SchoolCategorysViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
