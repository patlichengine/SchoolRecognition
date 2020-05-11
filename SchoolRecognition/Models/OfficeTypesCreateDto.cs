using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class OfficeTypesCreateDto
    {
        public Guid Id { get; set; }
        public string TypeDescription { get; set; }
        public bool? IsActive { get; set; }
    }
}
