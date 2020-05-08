using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class OfficesViewDto
    {
        public Guid Id { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public Guid? StateId { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }
        public byte[] OfficeImage { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public Guid? OfficeTypeID { get; set; }
    }
}
