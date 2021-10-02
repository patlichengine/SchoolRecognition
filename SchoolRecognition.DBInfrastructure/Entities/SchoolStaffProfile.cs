using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SchoolStaffProfile
    {
        public SchoolStaffProfile()
        {
            SchoolStaffDegrees = new HashSet<SchoolStaffDegree>();
        }

        public Guid Id { get; set; }
        public Guid? SchoolProfileId { get; set; }
        public int? TitleId { get; set; }
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public string ContactAddress { get; set; }
        public DateTime? DateEmployed { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public Guid? SchoolStaffCategoryId { get; set; }
        public bool? HasTrcn { get; set; }
        public string Trcn { get; set; }
        public bool IsBarred { get; set; }

        public virtual SchoolProfile SchoolProfile { get; set; }
        public virtual SchoolStaffCategory SchoolStaffCategory { get; set; }
        public virtual Title Title { get; set; }
        public virtual ICollection<SchoolStaffDegree> SchoolStaffDegrees { get; set; }
    }
}
