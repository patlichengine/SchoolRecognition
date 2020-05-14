using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class StatesViewDto
    {
        public Guid Id { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
    }
}
