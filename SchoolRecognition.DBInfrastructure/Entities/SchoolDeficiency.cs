using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SchoolDeficiency
    {
        public Guid Id { get; set; }
        public string AdditionalInfo { get; set; }
        public Guid? SchoolRecognitionTrailId { get; set; }
        public Guid? CapturedBy { get; set; }
        public bool IsResolved { get; set; }
        public Guid? FacilitySettingId { get; set; }
    }
}
