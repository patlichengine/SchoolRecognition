using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class FacilityItemsCreateDto
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsSummary { get; set; }

    }
}
