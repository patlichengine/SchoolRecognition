using SchoolRecognition.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class StatesViewPagedListLocalGovernmentsDto
    {
        public Guid Id { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public int LocalGovernmentsCount { get; set; }
        public int OfficeStatesCount { get; set; }
        public int SchoolsCount { get; set; }
        public virtual CustomPagedList<LocalGovernmentsViewDto> StateLGAs { get; set; }
        public virtual IEnumerable<OfficeStatesViewDto> StateOfficeStates { get; set; }
    }
}
