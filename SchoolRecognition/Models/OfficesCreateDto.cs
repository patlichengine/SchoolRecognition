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
        public Guid? StateId { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public Guid? CreatedBy { get; set; }
        public byte[] OfficeImage { get; set; }
        public double? Longitute { get; set; }
        public double? Latitude { get; set; }
        public Guid? OfficeTypeId { get; set; }
        //StateAssigned id the id of the State the Office is assigned to
        //which is used to auto-generate the OfficeStates entity
        public Guid? StateAssigned { get; set; }
    }
}
