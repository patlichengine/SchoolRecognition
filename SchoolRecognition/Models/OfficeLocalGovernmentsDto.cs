using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{

    public class OfficeLocalGovernmentsCreateDto
    {
        public Guid Id { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public Guid LocalGovernmentId { get; set; }
        public Guid OfficeId { get; set; }
    }

    public class OfficeLocalGovernmentsCreateMultipleDto
    {
        public Guid Id { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public List<Guid> LocalGovernmentIds { get; set; }
        public Guid OfficeId { get; set; }
    }
    public class OfficeLocalGovernmentsViewDto
    {
        public Guid Id { get; set; }
        public Guid LocalGovernmentId { get; set; }
        public string LocalGovernmentName { get; set; }
        public string LocalGovernmentCode { get; set; }
        public Guid OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string LocalGovernmentLocated { get; set; }
        public string OfficeTypeDescription { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
    }


    public class OfficeLocalGovernmentsCreationDependecyDto
    {
        public IEnumerable<StatesViewDto> States { get; set; }
        public IEnumerable<OfficeTypesViewDto> OfficeTypes { get; set; }
    }
}
