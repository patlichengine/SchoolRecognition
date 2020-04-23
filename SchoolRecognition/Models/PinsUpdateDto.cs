using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinsUpdateDto
    {
        public Guid Id { get; set; }
        public Guid? RecognitionTypeId { get; set; }
        public string SerialNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsInUse { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
