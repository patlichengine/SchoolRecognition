using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SchoolRecognitionTrailRejection
    {
        public Guid Id { get; set; }
        public Guid? SchoolRecognitionTrail { get; set; }
        public Guid? RejectedBy { get; set; }
        public string ReasonForRejection { get; set; }
        public DateTime DateRejected { get; set; }
    }
}
