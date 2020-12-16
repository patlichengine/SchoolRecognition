using Newtonsoft.Json;
using SchoolRecognition.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class RanksDto
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Rank Name is Required")]
        [MinLength(3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Unique Rank Code is Required")]
        [MaxLength(10)]
        public string Code { get; set; }
        [JsonIgnore]
        public IEnumerable<ApplicationUsers> ApplicationUsers { get; set; }

    }
}
