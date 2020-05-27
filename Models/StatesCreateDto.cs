using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class StatesCreateDto
    {
        public Guid Id { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
    }
}
