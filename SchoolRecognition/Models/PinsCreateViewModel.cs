using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinsCreateViewModel
    {
        public Guid? RecognitionTypeId { get; set; }
        public int NoOfPinToGenerate { get; set; }
    }
}
