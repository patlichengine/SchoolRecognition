using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class LocalGovernmentsViewDto
    {
        public Guid Id { get; set; }
        public string LgaName { get; set; }
        public string LgaCode { get; set; }
        public string StateName { get; set; }
        public int SchoolsCount { get; set; }
    }
    public class UpdateLocalGovernmentsDto
    {
        public string LgaName { get; set; }
        public string LgaCode { get; set; }
        public string StateName { get; set; }
        public int SchoolsCount { get; set; }
    }
}
