using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class OfficeStatesCreateDto
    {
        public Guid Id { get; set; }
        public Guid? StateId { get; set; }
        public Guid? OfficeId { get; set; }
    }
}
