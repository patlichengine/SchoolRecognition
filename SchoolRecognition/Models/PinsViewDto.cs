using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinsViewDto
    {
        public Guid Id { get; set; }
        public String RecognitionTypeName { get; set; }
        public string SerialPin { get; set; }
        public bool IsActive { get; set; }
        public bool IsInUse { get; set; }
        public String CreatedByName { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
