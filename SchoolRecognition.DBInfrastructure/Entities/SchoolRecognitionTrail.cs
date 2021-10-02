using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SchoolRecognitionTrail
    {
        public Guid Id { get; set; }
        public Guid? SchoolPaymentId { get; set; }
        public string OriginalName { get; set; }
        public string PreferredName { get; set; }
        public Guid? OfficeId { get; set; }
        public Guid? RequestedBy { get; set; }
        public Guid? InspectedBy { get; set; }
        public Guid? SupervisedBy { get; set; }
        public Guid? ReviewedBy { get; set; }
        public Guid? ApprovedBy { get; set; }
        public bool IsClosed { get; set; }
        public string RequestedSubjects { get; set; }
        public string EjectedSubjects { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime? DateInspected { get; set; }
        public DateTime? DateSupervised { get; set; }
        public DateTime? DateReviewed { get; set; }
        public DateTime? DateApproved { get; set; }
    }
}
