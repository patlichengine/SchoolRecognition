using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class OfficeCreationDependecyDto
    {
        public IEnumerable<StatesViewDto> States { get; set; }
        public IEnumerable<OfficeTypesViewDto> OfficeTypes { get; set; }
    }
}
