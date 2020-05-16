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
        public int LocalGovernmentsCount { get; set; }
        public int OfficeStatesCount { get; set; }
        public virtual IEnumerable<LocalGovernmentsViewDto> StateLGAs { get; set; }
        public virtual IEnumerable<OfficeStatesViewDto> StateOfficeStates { get; set; }
    }
}
