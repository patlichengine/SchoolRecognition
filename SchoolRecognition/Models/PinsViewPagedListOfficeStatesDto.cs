
using SchoolRecognition.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinsViewPagedListOfficeStatesDto
    {
        public Guid Id { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string StateName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedByUser { get; set; }
        public byte[] OfficeImage { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string OfficeTypeDescription { get; set; }
        public virtual CustomPagedList<OfficeStatesViewDto> StateOffices { get; set; }
        //public virtual IEnumerable<SchoolsViewDto> OfficeSchools { get; set; }
    }
}
