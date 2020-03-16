using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinsCreateViewModel
    {
        public Guid Id { get; set; }
        public Guid? RecognitionTypeId { get; set; }
        public string SerialPin { get; set; }
        public bool IsActive { get; set; }
        public bool IsInUse { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        ///
        public int NoOfPinToGenerate { get; set; }
    }
}
