using SchoolRecognition.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class ClassSettingsCreateDto
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "A Class Settings name is required")]
        [MaxLength(50)]
        public string Name { get; set; }



    }
}
