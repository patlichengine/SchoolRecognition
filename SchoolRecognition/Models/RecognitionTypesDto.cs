using AutoMapper;
using SchoolRecognition.Entities;
using SchoolRecognition.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class RecognitionTypesCreateDto
    {
        public Guid Id { get; set; }
        public string RecognitionTypeName { get; set; }
        public string RecognitionTypeCode { get; set; }
        //public virtual IEnumerable<PinsViewDto> RecognitionTypePins { get; set; }
    }

    public class RecognitionTypesViewDto
    {
        public Guid Id { get; set; }
        public string RecognitionTypeName { get; set; }
        public string RecognitionTypeCode { get; set; }
        public Int64 TotalPins { get; set; }
        public long TotalActivePins { get; set; }
        public long TotalInUsePins { get; set; }
        public virtual IEnumerable<PinsViewDto> RecognitionTypePins { get; set; }
        public virtual ICollection<Pins> Pins { get; set; } = new HashSet<Pins>();
    }
    public class RecognitionTypesViewPagedListPinsDto
    {

        public Guid Id { get; set; }
        public string RecognitionTypeName { get; set; }
        public string RecognitionTypeCode { get; set; }
        public Int64 TotalPins { get; set; }
        public long TotalActivePins { get; set; }
        public long TotalInUsePins { get; set; }
        public virtual PagedList<PinsViewDto> RecognitionTypePins { get; set; }
    }
}
