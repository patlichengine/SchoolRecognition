using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class Location
    {
        public int Id { get; set; }
        public string CentreNo { get; set; }
        public string CentreName { get; set; }
        public string CentreAddress { get; set; }
        public string CentreDirections { get; set; }
        public string CentreType { get; set; }
        public string CentreStatus { get; set; }
        public decimal Gpslong { get; set; }
        public decimal Gpslat { get; set; }
        public string Image { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string Landmark { get; set; }
        public int? LocationCategoryId { get; set; }
        public bool IsApproved { get; set; }
        public string StateCode { get; set; }
        public Guid? CapturedBy { get; set; }
        public Guid? ApprovedBy { get; set; }

        public virtual ApplicationUser ApprovedByNavigation { get; set; }
        public virtual ApplicationUser CapturedByNavigation { get; set; }
        public virtual Centre CentreNoNavigation { get; set; }
        public virtual LocationCategory LocationCategory { get; set; }
    }
}
