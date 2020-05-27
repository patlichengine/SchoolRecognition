using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class OfficeStatesCreateDto
    {
        public Guid Id { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public Guid StateId { get; set; }
        public Guid OfficeId { get; set; }
    }
    public class OfficeStatesCreateMultipleDto
    {
        public Guid Id { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public List<Guid> StateIds { get; set; }
        public Guid OfficeId { get; set; }
    }
    public class OfficeStatesViewDto
    {
        public Guid Id { get; set; }
        public Guid StateId { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public Guid OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string StateLocated { get; set; }
        public string OfficeTypeDescription { get; set; }
    }


    public class OfficeStatesCreationDependecyDto
    {
        public IEnumerable<StatesViewDto> States { get; set; }
        public IEnumerable<OfficeTypesViewDto> OfficeTypes { get; set; }
    }

}
