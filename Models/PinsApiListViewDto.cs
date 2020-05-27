using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinsApiListViewDto
    {
        public virtual IEnumerable<PinsViewDto> RecognitionTypePins { get; set; }
        public Int64 RangeFrom { get; set; }
        public Int64 RangeTo { get; set; }
        public Int64 RangeTotalPins { get; set; }
    }
}
