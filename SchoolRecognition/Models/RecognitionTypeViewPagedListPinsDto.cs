using SchoolRecognition.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class RecognitionTypeViewPagedListPinsDto
    {
        public Guid Id { get; set; }
        public string RecognitionTypeName { get; set; }
        public string RecognitionTypeCode { get; set; }
        public virtual CustomPagedList<PinsViewDto> RecognitionTypePins { get; set; }
    }
}
