using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class OfficesCreateDto
    {

        public Guid Id { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string StateName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedByUser { get; set; }
        public byte[] OfficeImage { get; set; }
        public double? Longitute { get; set; }
        public double? Latitude { get; set; }
        public Guid? OfficeTypeId { get; set; }
    }
}
