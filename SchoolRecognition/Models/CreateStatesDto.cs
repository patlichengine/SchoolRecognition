using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class CreateStatesDto
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage= "State Name is Required")]
        [MinLength(3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Unique State Code is Required")]
        [MaxLength(3)]
        public string Code { get; set; }
    }
}
