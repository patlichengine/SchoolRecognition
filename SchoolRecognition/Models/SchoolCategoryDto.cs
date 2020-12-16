using SchoolRecognition.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class SchoolCategoryDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "A Category name is required")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "A Unique Code For Category is Required")]
        [MaxLength(1)]
        public string Code { get; set; }
        // public Schools Schools { get; set; }

        [JsonIgnore]
        public IEnumerable<Centres> Centres { get; set; }
        public IEnumerable<Schools> Schools { get; set; }
    }
}
