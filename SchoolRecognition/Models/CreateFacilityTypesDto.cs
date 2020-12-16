using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class CreateFacilityTypesDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsGlobal { get; set; }

    }
}
