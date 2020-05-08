using SchoolRecognition.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinViewPagedListPinHistoriesDto
    {
        public Guid Id { get; set; }
        public String RecognitionTypeName { get; set; }
        public string SerialNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsInUse { get; set; }
        public String CreatedByUser { get; set; }
        public DateTime? DateCreated { get; set; }
        //public virtual CustomPagedList<PinHistoriesViewDto> PinHistories { get; set; }
        //public virtual ICollection<SchoolPayments> SchoolPayments { get; set; }
    }
}
