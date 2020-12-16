using SchoolRecognition.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class LocalGovernmentsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? StateId { get; set; }
        public States State { get; set; }

        public IEnumerable<Schools> Schools { get; set; }
    }
}
